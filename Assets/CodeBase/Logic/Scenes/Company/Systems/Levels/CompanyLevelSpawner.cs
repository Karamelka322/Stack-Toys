using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Levels
{
    public class CompanyLevelSpawner : ICompanyLevelSpawner
    {
        private readonly ICompanyLevelFactory _levelFactory;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly ILevelProvider _levelProvider;

        public BoolReactiveProperty IsLoaded { get; }
        
        public CompanyLevelSpawner(
            ICompanyLevelFactory levelFactory, 
            ILevelProvider levelProvider,
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider)
        {
            _levelProvider = levelProvider;
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _levelFactory = levelFactory;
            IsLoaded = new BoolReactiveProperty();
            
            LoadAsync().Forget();
        }
        
        private async UniTask LoadAsync()
        {
            var targetLevel = _companyLevelsSaveDataProvider.GetCurrentLevel();
            var level = await _levelFactory.SpawnAsync(targetLevel);
            
            _levelProvider.Register(level);
            
            IsLoaded.Value = true;
        }
    }
}