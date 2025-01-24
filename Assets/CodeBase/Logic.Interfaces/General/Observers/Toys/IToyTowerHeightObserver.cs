using UniRx;

namespace CodeBase.Logic.General.Observers.Toys
{
    public interface IToyTowerHeightObserver
    {
        ReactiveProperty<float> TowerHeight { get; }
    }
}