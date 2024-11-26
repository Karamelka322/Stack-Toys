using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Load
{
    public class CompanySceneLoad : ICompanySceneLoad
    {
        private readonly ILevelFactory _levelFactory;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        
        public BoolReactiveProperty IsLoaded { get; }
        
        public CompanySceneLoad(
            ILevelFactory levelFactory, 
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider)
        {
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _levelFactory = levelFactory;
            IsLoaded = new BoolReactiveProperty();
            
            LoadAsync().Forget();
        }
        
        private async UniTask LoadAsync()
        {
            var targetLevel = _companyLevelsSaveDataProvider.GetTargetLevel();
            
            _companyLevelsSaveDataProvider.SetCurrentLevel(targetLevel);
            
            var level = await _levelFactory.SpawnAsync(targetLevel);
            // var toy = await _toyFactory.SpawnAsync(level.ToyPoint.position);
            
            IsLoaded.Value = true;
        }
    }
}