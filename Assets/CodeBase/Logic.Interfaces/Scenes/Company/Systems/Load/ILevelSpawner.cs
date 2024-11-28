using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load
{
    public interface ILevelSpawner
    {
        BoolReactiveProperty IsLoaded { get; }
    }
}