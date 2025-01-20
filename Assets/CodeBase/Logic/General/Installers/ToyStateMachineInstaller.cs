using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.StateMachines.Toys.States;
using CodeBase.Logic.General.StateMachines.Toys.Transitions;
using CodeBase.Logic.General.Unity.Toys;
using JetBrains.Annotations;
using Zenject;

namespace CodeBase.Logic.General.Installers
{
    [UsedImplicitly]
    public class ToyStateMachineInstaller : Installer<ToyStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<ToyMediator, ToyBabbleState, ToyBabbleState.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyRotateState, ToyRotateState.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyDragState, ToyDragState.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyTowerState, ToyTowerState.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyIdleState, ToyIdleState.Factory>().AsSingle();
            
            Container.BindFactory<ToyMediator, ToySelectTransition, ToySelectTransition.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyStartDragTransition, ToyStartDragTransition.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ClickUpTransition, ClickUpTransition.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyRotationTransition, ToyRotationTransition.Factory>().AsSingle();
            Container.BindFactory<ToyTowerTransition, ToyTowerTransition.Factory>().AsSingle();
            
            Container.BindFactory<ToyMediator, ToyStateMachine, ToyStateMachine.Factory>().AsSingle();
        }
    }
}