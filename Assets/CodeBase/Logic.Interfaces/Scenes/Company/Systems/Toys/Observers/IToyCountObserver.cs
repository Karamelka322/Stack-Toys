using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.Observers
{
    public interface IToyCountObserver
    {
        IntReactiveProperty LeftAvailableNumberOfToys { get; }
        IntReactiveProperty NumberOfOpenToys { get; }
        IntReactiveProperty MaxNumberOfToys { get; }
        IntReactiveProperty NumberOfTowerBuildToys { get; }
    }
}