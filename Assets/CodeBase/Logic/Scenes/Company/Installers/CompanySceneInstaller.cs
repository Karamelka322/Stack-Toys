using CodeBase.Logic.General.Factories.Babble;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.General.Providers.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.General.Providers.Data.ScriptableObjects.Levels;
using CodeBase.Logic.General.Providers.Objects.Canvases;
using CodeBase.Logic.General.Providers.Objects.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Factories.Levels;
using CodeBase.Logic.Scenes.Company.Factories.Toys;
using CodeBase.Logic.Scenes.Company.Presenters.Toys;
using CodeBase.Logic.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Scenes.Company.Systems.Cameras;
using CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine;
using CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.States;
using CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.Transitions;
using CodeBase.Logic.Scenes.Company.Systems.Finish;
using CodeBase.Logic.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Scenes.Company.Systems.Load;
using CodeBase.Logic.Scenes.Company.Systems.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions;
using CodeBase.UI.Scenes.Company.Factories.Elements.Toys;
using CodeBase.UI.Scenes.Company.Factories.Windows.Finish;
using CodeBase.UI.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Scenes.Company.Mediators.Elements.Toys;
using CodeBase.UI.Scenes.Company.Presenters.Windows;
using CodeBase.UI.Scenes.Company.Windows.Finish;
using CodeBase.UI.Scenes.Company.Windows.Main;
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
            BindPresenters();
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
            
            // Game - Effects
            Container.BindInterfacesTo<ToySelectEffectFactory>().AsSingle();
            
            // UI - Elements
            Container.BindInterfacesTo<ToyRotatorFactory>().AsSingle();
            Container.BindFactory<Transform, ToyRotatorMediator, ToyRotatorElement, ToyRotatorElement.Factory>().AsSingle();
            
            // UI - Windows
            Container.BindInterfacesTo<CompanyMainWindowFactory>().AsSingle();
            Container.BindInterfacesTo<CompanyFinishWindowFactory>().AsSingle();
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
            Container.BindInterfacesTo<ToyProvider>().AsSingle();
        }

        private void BindPresenters()
        {
            // Game - Effects
            Container.Bind<ToySelectEffectPresenter>().AsSingle().NonLazy();
            
            // UI - Windows
            Container.Bind<CompanyMainWindowPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CompanyFinishWindowPresenter>().AsSingle().NonLazy();
        }

        private void BindSystems()
        {
            Container.BindInterfacesTo<CompanySceneLoad>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CameraRenderSetup>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LevelBorderSystem>().AsSingle();
            
            // Observers
            Container.BindInterfacesTo<ToySelectObserver>().AsSingle();
            Container.BindInterfacesTo<ToyTowerObserver>().AsSingle();
            Container.BindInterfacesTo<FinishObserver>().AsSingle();
            
            // Toy
            Container.BindFactory<ToyMediator, ToyBabbleState, ToyBabbleState.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyRotateState, ToyRotateState.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyDragState, ToyDragState.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyTowerState, ToyTowerState.Factory>().AsSingle();
            
            Container.BindFactory<ToyMediator, ToySelectTransition, ToySelectTransition.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyStartDragTransition, ToyStartDragTransition.Factory>().AsSingle();
            Container.BindFactory<ToyMediator, ToyEndDragTransition, ToyEndDragTransition.Factory>().AsSingle();
            Container.BindFactory<ToyTowerTransition, ToyTowerTransition.Factory>().AsSingle();
            
            Container.BindFactory<ToyMediator, ToyStateMachine, ToyStateMachine.Factory>().AsSingle();
            Container.Bind<ToySpawner>().AsSingle().NonLazy();

            // Camera
            Container.BindFactory<Camera, CameraScrollState, CameraScrollState.Factory>().AsSingle();
            Container.BindFactory<Camera, CameraToyFollowState, CameraToyFollowState.Factory>().AsSingle();
            Container.BindFactory<CameraToySelectTransition, CameraToySelectTransition.Factory>().AsSingle();
            Container.BindFactory<CameraToyUnselectTransition, CameraToyUnselectTransition.Factory>().AsSingle();
            Container.Bind<CameraStateMachine>().AsSingle().NonLazy();
            
            // UI - Windows
            Container.BindInterfacesTo<CompanyMainWindow>().AsSingle();
            Container.BindInterfacesTo<CompanyFinishWindow>().AsSingle();
        }
    }
}