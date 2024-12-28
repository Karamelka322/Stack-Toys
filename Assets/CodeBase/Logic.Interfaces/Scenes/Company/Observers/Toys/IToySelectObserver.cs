using CodeBase.Logic.General.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Observers.Toys
{
    public interface IToySelectObserver
    {
        ReactiveProperty<ToyMediator> Toy { get; }
    }
}