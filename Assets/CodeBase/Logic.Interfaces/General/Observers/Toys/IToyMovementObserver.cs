using CodeBase.Logic.General.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.General.Observers.Toys
{
    public interface IToyMovementObserver
    {
        /// <summary>
        /// Список игрушек которые находятся за переделами камеры
        /// </summary>
        IReadOnlyReactiveCollection<ToyMediator> ToysOutsideCameraFieldOfView { get; }
    }
}