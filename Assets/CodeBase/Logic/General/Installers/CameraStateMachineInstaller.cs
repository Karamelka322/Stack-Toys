using CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine;
using CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.States;
using CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.Transitions;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.General.Installers
{
    [UsedImplicitly]
    public class CameraStateMachineInstaller : Installer<CameraStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            // States
            Container.BindFactory<Camera, CameraScrollState, CameraScrollState.Factory>().AsSingle();
            Container.BindFactory<Camera, CameraToyFollowState, CameraToyFollowState.Factory>().AsSingle();
            
            // Transitions
            Container.BindFactory<CameraToySelectTransition, CameraToySelectTransition.Factory>().AsSingle();
            Container.BindFactory<CameraToyUnselectTransition, CameraToyUnselectTransition.Factory>().AsSingle();
            
            Container.BindInterfacesTo<CameraStateMachine>().AsSingle().NonLazy();
        }
    }
}