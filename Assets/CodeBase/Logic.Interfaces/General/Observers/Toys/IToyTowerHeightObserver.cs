using UniRx;

namespace CodeBase.Logic.Interfaces.General.Observers.Toys
{
    public interface IToyTowerHeightObserver
    {
        ReactiveProperty<float> TowerHeight { get; }
    }
}