using System;
using CodeBase.Logic.General.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Observers.Toys
{
    public interface IToyTowerObserver
    {
        ReactiveCollection<ToyMediator> Tower { get; }
        event Action OnTowerFallen;
    }
}