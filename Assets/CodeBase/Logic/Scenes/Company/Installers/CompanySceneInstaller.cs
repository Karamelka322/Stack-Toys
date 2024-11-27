using CodeBase.Logic.General.Commands;
using CodeBase.Logic.General.Factories.Babble;
using CodeBase.Logic.General.Factories.Finish;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.General.Providers.Data.Saves;
using CodeBase.Logic.General.Providers.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.General.Providers.Data.ScriptableObjects.Levels;
using CodeBase.Logic.General.Providers.Objects.Canvases;
using CodeBase.Logic.General.Providers.Objects.Toys;
using CodeBase.Logic.General.Systems.Finish;
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
using CodeBase.UI.General.Factories.Windows.Pause;
using CodeBase.UI.General.Windows.Pause;
using CodeBase.UI.Scenes.Company.Factories.Elements.Toys;
using CodeBase.UI.Scenes.Company.Factories.Windows.Finish;
using CodeBase.UI.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Scenes.Company.Mediators.Elements.Toys;
using CodeBase.UI.Scenes.Company.Presenters.Windows;
using CodeBase.UI.Scenes.Company.Windows.Finish;
using CodeBase.UI.Scenes.Company.Windows.Main;
using UnityEngine;
using Zenject;

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
            Container.BindInterfacesTo<ClickCommand>().AsSingle();
        }

        private void BindFactories()
        {
            // Game - Objects
            Container.BindInterfacesTo<LevelFactory>().AsSingle();
            Container.BindInterfacesTo<ToyFactory>().AsSingle();
            Container.BindInterfacesTo<BabbleFactory>().AsSingle();
            Container.BindInterfacesTo<FinishLineFactory>().AsSingle();
            
            // Game - Effects
            Container.BindInterfacesTo<ToySelectEffectFactory>().AsSingle();
            
            // UI - Elements
            Container.BindInterfacesTo<ToyRotatorFactory>().AsSingle();
            Container.BindFactory<Transform, ToyRotatorMediator, ToyRotatorElement, ToyRotatorElement.Factory>().AsSingle();
            
            // UI - Windows
            Container.BindInterfacesTo<CompanyMainWindowFactory>().AsSingle();
            Container.BindInterfacesTo<CompanyFinishWindowFactory>().AsSingle();
            Container.BindInterfacesTo<PauseWindowFactory>().AsSingle();
        }

        private void BindProviders()
        {
            // Data - ScriptableObjects
            Container.BindInterfacesTo<LevelsConfigProvider>().AsSingle();
            Container.BindInterfacesTo<CameraSettingsProvider>().AsSingle();
            
            // Data - Saves
            Container.BindInterfacesTo<CompanyLevelsSaveDataProvider>().AsSingle();
            
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
            Container.BindInterfacesTo<CompanySceneUnload>().AsSingle();
            Container.BindInterfacesTo<CameraRenderSetup>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FinishSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LevelBorderSystem>().AsSingle();
            Container.BindInterfacesTo<FinishLineSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CameraBorderSystem>().AsSingle();

            // Observers
            Container.BindInterfacesTo<ToyCountObserver>().AsSingle();
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
            Container.BindInterfacesTo<ToySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyDestroyer>().AsSingle().NonLazy();

            // Camera
            Container.BindFactory<Camera, CameraScrollState, CameraScrollState.Factory>().AsSingle();
            Container.BindFactory<Camera, CameraToyFollowState, CameraToyFollowState.Factory>().AsSingle();
            Container.BindFactory<CameraToySelectTransition, CameraToySelectTransition.Factory>().AsSingle();
            Container.BindFactory<CameraToyUnselectTransition, CameraToyUnselectTransition.Factory>().AsSingle();
            Container.BindInterfacesTo<CameraStateMachine>().AsSingle().NonLazy();
            
            // UI - Windows
            Container.BindInterfacesTo<CompanyMainWindow>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CompanyFinishWindow>().AsSingle().NonLazy();
            Container.Bind<PauseWindow>().AsSingle().NonLazy();
        }
    }
}