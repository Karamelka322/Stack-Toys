using CodeBase.Logic.General.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public interface IToySelectObserver
    {
        ReactiveProperty<ToyMediator> Toy { get; }
    }
}