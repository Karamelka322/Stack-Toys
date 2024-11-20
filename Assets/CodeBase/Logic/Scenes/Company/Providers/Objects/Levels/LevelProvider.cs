using CodeBase.Logic.Scenes.Company.Factories;
using CodeBase.Logic.Scenes.Company.Unity;

namespace CodeBase.Logic.Scenes.Company.Providers
{
    public class LevelProvider : ILevelProvider
    {
        private readonly ILevelFactory _levelFactory;

        public LevelMediator Level { get; private set; }

        public LevelProvider(ILevelFactory levelFactory)
        {
            _levelFactory = levelFactory;
            _levelFactory.OnSpawn += OnLevelSpawn;
        }

        private void OnLevelSpawn(LevelMediator level)
        {
            Level = level;
        }
    }
}