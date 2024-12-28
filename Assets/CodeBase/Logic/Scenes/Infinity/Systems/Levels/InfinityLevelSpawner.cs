using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Levels;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Levels;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Systems.Levels
{
    public class InfinityLevelSpawner : IInfinityLevelSpawner
    {
        private readonly IInfinityLevelFactory _infinityLevelFactory;
        private readonly ILevelProvider _levelProvider;

        public BoolReactiveProperty IsSpawned { get; }
        
        public InfinityLevelSpawner(ILevelProvider levelProvider, IInfinityLevelFactory infinityLevelFactory)
        {
            _levelProvider = levelProvider;
            _infinityLevelFactory = infinityLevelFactory;

            IsSpawned = new BoolReactiveProperty();
            
            SpawnAsync().Forget();
        }
        
        private async UniTask SpawnAsync()
        {
            var level = await _infinityLevelFactory.SpawnAsync();
            _levelProvider.Register(level);
            
            IsSpawned.Value = true;
        }
    }
}