using CodeBase.Logic.General.StateMachines.ToyChoicer;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using JetBrains.Annotations;
using Zenject;

namespace CodeBase.Logic.General.Installers
{
    [UsedImplicitly]
    public class ToyChoicerStateMachineInstaller : Installer<ToyChoicerStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<ToyChoicerMediator, ToyMediator, ToyMediator,
                ToyChoicerRotateState, ToyChoicerRotateState.Factory>().AsSingle();
            
            Container.BindFactory<ToyChoicerMediator, ToyMediator, ToyMediator,
                ToyChoicerStateMachine, ToyChoicerStateMachine.Factory>().AsSingle();
        }
    }
}