using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Observers.Toys
{
    public interface IToyCountObserver
    {
        IntReactiveProperty LeftAvailableNumberOfToys { get; }
        IntReactiveProperty NumberOfOpenToys { get; }
        IntReactiveProperty MaxNumberOfToys { get; }
        IntReactiveProperty NumberOfTowerBuildToys { get; }
    }
}