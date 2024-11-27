using System;
using CodeBase.Logic.General.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers
{
    public interface IToyTowerObserver
    {
        ReactiveCollection<ToyMediator> Tower { get; }
        event Action OnTowerFallen;
    }
}