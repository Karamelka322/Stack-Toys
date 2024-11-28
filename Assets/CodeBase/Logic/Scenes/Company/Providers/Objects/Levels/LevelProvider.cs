using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Scenes.Company.Unity;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Providers.Objects.Levels
{
    public class LevelProvider : ILevelProvider
    {
        public ReactiveProperty<LevelMediator> Level { get; }

        public LevelProvider()
        {
            Level = new ReactiveProperty<LevelMediator>();
        }

        public void Register(LevelMediator levelMediator)
        {
            Level.Value = levelMediator;
        }
    }
}