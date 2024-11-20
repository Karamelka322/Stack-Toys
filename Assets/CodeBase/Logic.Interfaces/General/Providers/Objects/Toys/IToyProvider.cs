using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using UniRx;

namespace CodeBase.Logic.General.Providers.Objects.Toys
{
    public interface IToyProvider
    {
        ReactiveCollection<(ToyMediator, ToyStateMachine)> Toys { get; }
    }
}