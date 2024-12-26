using CodeBase.Logic.General.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers
{
    public interface IToyMovementObserver
    {
        /// <summary>
        /// Список игрушек которые находятся за переделами камеры
        /// </summary>
        IReadOnlyReactiveCollection<ToyMediator> ToysOutsideCameraFieldOfView { get; }
    }
}