using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Levels
{
    public interface IInfinityLevelSpawner
    {
        BoolReactiveProperty IsSpawned { get; }
    }
}