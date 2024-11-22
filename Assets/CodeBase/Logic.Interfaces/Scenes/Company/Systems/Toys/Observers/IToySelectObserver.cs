using CodeBase.Logic.General.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers
{
    public interface IToySelectObserver
    {
        ReactiveProperty<ToyMediator> Toy { get; }
    }
}