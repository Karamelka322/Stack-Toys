using CodeBase.Logic.Scenes.Company.Unity;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels
{
    public interface ILevelProvider
    {
        ReactiveProperty<LevelMediator> Level { get; }
        void Register(LevelMediator levelMediator);
    }
}