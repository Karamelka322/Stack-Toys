using System;
using CodeBase.Logic.General.Unity.Toys;

namespace CodeBase.Logic.Interfaces.General.Observers.Toys
{
    public interface IToyClickObserver
    {
        void OnClickDownAsObservableAdd(ToyMediator toyMediator, Action action);
        void OnClickDownAsObservableRemove(ToyMediator toyMediator);
    }
}