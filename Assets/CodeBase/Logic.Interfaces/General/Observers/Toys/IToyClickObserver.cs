using System;
using CodeBase.Logic.General.Unity.Toys;

namespace CodeBase.Logic.General.StateMachines.Toys.Transitions
{
    public interface IToyClickObserver
    {
        void OnClickDownAsObservableAdd(ToyMediator toyMediator, Action action);
        void OnClickDownAsObservableRemove(ToyMediator toyMediator);
    }
}