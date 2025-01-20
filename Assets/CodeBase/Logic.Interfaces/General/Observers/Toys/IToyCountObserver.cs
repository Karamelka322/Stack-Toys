using UniRx;

namespace CodeBase.Logic.Interfaces.General.Observers.Toys
{
    public interface IToyCountObserver
    {
        IntReactiveProperty LeftAvailableNumberOfToys { get; }
        IntReactiveProperty NumberOfOpenToys { get; }
        IntReactiveProperty MaxNumberOfToys { get; }
        IntReactiveProperty NumberOfTowerBuildToys { get; }
    }
}