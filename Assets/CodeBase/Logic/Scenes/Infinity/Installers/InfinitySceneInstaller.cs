using CodeBase.Logic.General.Commands;
using CodeBase.Logic.General.Factories.Babble;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.General.Formulas;
using CodeBase.Logic.General.Providers.Objects.Canvases;
using CodeBase.Logic.General.Providers.Objects.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Factories.Toys;
using CodeBase.Logic.Scenes.Company.Observers.Toys;
using CodeBase.Logic.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Scenes.Company.Systems.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions;
using CodeBase.Logic.Scenes.Infinity.Factories.Levels;
using CodeBase.Logic.Scenes.Infinity.Factories.Toys;
using CodeBase.Logic.Scenes.Infinity.Observers.Ready;
using CodeBase.Logic.Scenes.Infinity.Observers.Toys;
using CodeBase.Logic.Scenes.Infinity.Providers.Data;
using CodeBase.Logic.Scenes.Infinity.Providers.Objects;
using CodeBase.Logic.Scenes.Infinity.Systems.Levels;
using CodeBase.Logic.Scenes.Infinity.Systems.Toys;
using CodeBase.UI.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Scenes.Company.Presenters.Windows;
using CodeBase.UI.Scenes.Company.Windows.Main;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Logic.Scenes.Infinity.Installers
{
    public class InfinitySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFormulas();
            BindProviders();
            BindFactories();
            BindPresenters();
            BindSystems();
            BindObservers();
        }

        private void BindFormulas()
        {
            Container.BindInterfacesTo<ClickFormulas>().AsSingle();
            Container.BindInterfacesTo<RayFormulas>().AsSingle();
            Container.BindInterfacesTo<EdgeFormulas>().AsSingle();
        }

        private void BindProviders()
        {
            // Objects
            Container.BindInterfacesTo<LevelProvider>().AsSingle();
            Container.BindInterfacesTo<ToyProvider>().AsSingle();
            Container.BindInterfacesTo<ToyChoicerProvider>().AsSingle();
            Container.BindInterfacesTo<WindowCanvasProvider>().AsSingle();
            
            // Data - ScriptableObjects
            Container.BindInterfacesTo<InfinitySceneToySettingsProvider>().AsSingle();
        }

        private void BindFactories()
        {
            // Game
            Container.BindInterfacesTo<InfinityLevelFactory>().AsSingle();
            Container.BindInterfacesTo<ToyFactory>().AsSingle();
            Container.BindInterfacesTo<ToyChoicerFactory>().AsSingle();
            Container.BindInterfacesTo<BabbleFactory>().AsSingle();
            
            // UI
            Container.BindFactory<Slider, CanvasGroup, ToyRotatorElement, ToyRotatorElement.Factory>().AsSingle();
            Container.BindInterfacesTo<CompanyMainWindowFactory>().AsSingle();
            Container.BindInterfacesTo<ToyShadowFactory>().AsSingle();
            Container.BindInterfacesTo<ToySelectEffectFactory>().AsSingle();
        }

        private void BindPresenters()
        {
            Container.BindInterfacesTo<CompanyLoadingWindowPresenter>().AsSingle().NonLazy();
            Container.Bind<CompanyMainWindowPresenter>().AsSingle().NonLazy();
        }

        private void BindObservers()
        {
            Container.BindInterfacesTo<InfinitySceneReadyObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyChoiceObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToySelectObserver>().AsSingle();
            Container.BindInterfacesTo<InfinityToyCountObserver>().AsSingle();
            // Container.BindInterfacesTo<ToyTowerObserver>().AsSingle();
            // Container.BindInterfacesTo<ToyCollisionObserver>().AsSingle();
            // Container.BindInterfacesTo<ToyMovementObserver>().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesTo<InfinityLevelBorderSystem>().AsSingle();
            Container.BindInterfacesTo<InfinityLevelSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InfinityToySpawner>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<ToyShadowSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyOutlineSystem>().AsSingle().NonLazy();

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
            
            Container.BindInterfacesTo<CompanyMainWindow>().AsSingle().NonLazy();
        }
    }
}