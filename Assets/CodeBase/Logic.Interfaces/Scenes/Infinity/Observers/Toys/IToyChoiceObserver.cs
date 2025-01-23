using System;
using CodeBase.Logic.General.StateMachines.ToyChoicer;
using CodeBase.Logic.General.Unity.Toys;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Observers.Toys
{
    public interface IToyChoiceObserver
    {
        event Action<ToyChoicer, ToyMediator> OnChoice;
    }
}