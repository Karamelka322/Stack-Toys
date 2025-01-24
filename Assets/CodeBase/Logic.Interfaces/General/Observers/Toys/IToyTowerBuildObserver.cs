using System;
using CodeBase.Logic.General.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.General.Observers.Toys
{
    public interface IToyTowerBuildObserver
    {
        ReactiveCollection<ToyMediator> Tower { get; }
        event Action OnTowerFallen;
    }
}