using CodeBase.Logic.General.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.General.Observers.Toys
{
    public interface IToySelectObserver
    {
        ReactiveProperty<ToyMediator> Toy { get; }
    }
}