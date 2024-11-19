using CodeBase.Data.ScriptableObjects.Levels;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.General.Providers.ScriptableObjects.Cameras;
using CodeBase.Logic.General.Systems;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Factories;
using CodeBase.Logic.Scenes.Company.Providers;
using CodeBase.Logic.Scenes.Company.Systems.Cameras;
using CodeBase.Logic.Scenes.Company.Systems.Load;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions;
using CodeBase.Logic.Services.Window;
using CodeBase.UI.Scenes.Company.Elements.Toys.Rotator;
using CodeBase.UI.Scenes.Company.Factories.Elements.Toys.Rotator;
using UnityEngine;
using Zenject;
using RaycastCommand = CodeBase.Logic.General.Commands.RaycastCommand;

namespace CodeBase.Logic.Scenes.Company.Installers
{
    public class CompanySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCommands();
            BindFactories();
            BindProviders();
            BindSystems();
        }

        private void BindCommands()
        {
            Container.BindInterfacesTo<RaycastCommand>().AsSingle();
        }

        private void BindFactories()
        {
            // Game - Objects
            Container.BindInterfacesTo<LevelFactory>().AsSingle();
            Container.BindInterfacesTo<ToyFactory>().AsSingle();
            Container.BindInterfacesTo<BabbleFactory>().AsSingle();
            
            // UI - Elements
            Container.BindInterfacesTo<ToyRotatorFactory>().AsSingle();
            Container.BindFactory<Transform, ToyRotatorMediator, ToyRotatorElement, ToyRotatorElement.Factory>().AsSingle();
        }

        private void BindProviders()
        {
            // ScriptableObjects
            Container.BindInterfacesTo<LevelsConfigProvider>().AsSingle();
            Container.BindInterfacesTo<CameraSettingsProvider>().AsSingle();
            
            // Objects
            Container.BindInterfacesTo<LevelProvider>().AsSingle();
            Container.BindInterfacesTo<WindowCanvasProvider>().AsSingle();
            Container.BindInterfacesTo<GameCanvasProvider>().AsSingle();
        }
        
        private void BindSystems()
        {
            Container.BindInterfacesTo<CompanySceneLoad>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyDragSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CameraScrolling>().AsSingle().NonLazy();
            
            Container.BindFactory<ToyMediator, ToyBabbleState, ToyBabbleState.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyRotateState, ToyRotateState.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToySelectTransition, ToySelectTransition.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyStateMachine, ToyStateMachine.Factory>().AsSingle();
        }
    }
}