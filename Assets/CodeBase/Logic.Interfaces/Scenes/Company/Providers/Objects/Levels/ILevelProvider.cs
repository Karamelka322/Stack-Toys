using CodeBase.Logic.Scenes.Company.Unity;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels
{
    public interface ILevelProvider
    {
        LevelMediator Level { get; }
    }
}