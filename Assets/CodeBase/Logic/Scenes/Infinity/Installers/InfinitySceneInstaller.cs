using CodeBase.Logic.General.Factories.Babble;
using CodeBase.Logic.General.Factories.Input;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.General.Formulas;
using CodeBase.Logic.General.Installers;
using CodeBase.Logic.General.Observers.Toys;
using CodeBase.Logic.General.Providers.Objects.Canvases;
using CodeBase.Logic.General.Providers.Objects.Toys;
using CodeBase.Logic.General.StateMachines.ToyChoicer;
using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.StateMachines.Toys.States;
using CodeBase.Logic.General.StateMachines.Toys.Transitions;
using CodeBase.Logic.General.Systems.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Factories.Toys;
using CodeBase.Logic.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Scenes.Infinity.Factories.Levels;
using CodeBase.Logic.Scenes.Infinity.Factories.Toys;
using CodeBase.Logic.Scenes.Infinity.Observers.Ready;
using CodeBase.Logic.Scenes.Infinity.Observers.Toys;
using CodeBase.Logic.Scenes.Infinity.Providers.Data;
using CodeBase.Logic.Scenes.Infinity.Providers.Objects;
using CodeBase.Logic.Scenes.Infinity.Systems.Levels;
using CodeBase.Logic.Scenes.Infinity.Systems.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
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
            
            Container.BindFactory<ToyChoicerMediator, ToyMediator, ToyMediator, ToyChoicer, ToyChoicer.Factory>().AsSingle();
            
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
            Container.BindInterfacesTo<ToyClickObserver>().AsSingle();
            Container.BindInterfacesTo<InfinityToyCountObserver>().AsSingle();
            Container.BindInterfacesTo<ToyTowerBuildObserver>().AsSingle();
            Container.BindInterfacesTo<ToyCollisionObserver>().AsSingle();
            Container.BindInterfacesTo<ToyMovementObserver>().AsSingle();
            Container.BindInterfacesTo<ToyTowerHeightObserver>().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesTo<InfinityLevelBorderSystem>().AsSingle();
            Container.BindInterfacesTo<InfinityLevelSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InfinityToySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InfinityToyDestroyer>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<ToyShadowSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyOutlineSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyBabbleSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyRotateAnimation>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyChoicerRotateAnimation>().AsSingle().NonLazy();

            ToyStateMachineInstaller.Install(Container);
            
            Container.BindInterfacesTo<CompanyMainWindow>().AsSingle().NonLazy();
        }
    }
}