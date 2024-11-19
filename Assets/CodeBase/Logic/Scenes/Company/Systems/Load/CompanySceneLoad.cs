using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.Scenes.Company.Factories;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Load
{
    public class CompanySceneLoad : ICompanySceneLoad
    {
        private readonly ILevelFactory _levelFactory;
        private readonly IToyFactory _toyFactory;

        public BoolReactiveProperty IsLoaded { get; }
        
        public CompanySceneLoad(ILevelFactory levelFactory, IToyFactory toyFactory)
        {
            _toyFactory = toyFactory;
            _levelFactory = levelFactory;
            IsLoaded = new BoolReactiveProperty();
            
            InitializeAsync().Forget();
        }
        
        private async UniTask InitializeAsync()
        {
            var level = await _levelFactory.SpawnAsync();
            var toy = await _toyFactory.SpawnAsync(level.ToyPoint.position);
            
            IsLoaded.Value = true;
        }
    }
}