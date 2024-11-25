using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.UI.Interfaces.General.Windows.Loading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Load
{
    public class CompanySceneLoad : ICompanySceneLoad
    {
        private readonly ILevelFactory _levelFactory;
        private readonly IToyFactory _toyFactory;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly ILoadingWindow _loadingWindow;

        public BoolReactiveProperty IsLoaded { get; }
        
        public CompanySceneLoad(
            ILevelFactory levelFactory,
            IToyFactory toyFactory,
            ILoadingWindow loadingWindow,
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider)
        {
            _loadingWindow = loadingWindow;
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _toyFactory = toyFactory;
            _levelFactory = levelFactory;
            IsLoaded = new BoolReactiveProperty();
            
            LoadAsync().Forget();
        }
        
        private async UniTask LoadAsync()
        {
            var targetLevel = _companyLevelsSaveDataProvider.GetTargetLevel();
            
            _companyLevelsSaveDataProvider.SetCurrentLevel(targetLevel);
            
            var level = await _levelFactory.SpawnAsync(targetLevel);
            var toy = await _toyFactory.SpawnAsync(level.ToyPoint.position);
            
            IsLoaded.Value = true;
        }
    }
}