using CodeBase.Logic.General.Factories.Babble;
using CodeBase.Logic.General.Factories.Confetti;
using CodeBase.Logic.General.Factories.Toys;
using CodeBase.Logic.General.Formulas;
using CodeBase.Logic.General.Installers;
using CodeBase.Logic.General.Observers.Toys;
using CodeBase.Logic.General.Providers.Data.Saves;
using CodeBase.Logic.General.Providers.Data.ScriptableObjects.Cameras;
using CodeBase.Logic.General.Providers.Data.ScriptableObjects.Levels;
using CodeBase.Logic.General.Providers.Objects.Canvases;
using CodeBase.Logic.General.Providers.Objects.Toys;
using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.General.Systems.Toys;
using CodeBase.Logic.General.Unity.Finish;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Factories.Finish;
using CodeBase.Logic.Scenes.Company.Factories.Levels;
using CodeBase.Logic.Scenes.Company.Factories.Toys;
using CodeBase.Logic.Scenes.Company.Observers.Finish;
using CodeBase.Logic.Scenes.Company.Observers.Ready;
using CodeBase.Logic.Scenes.Company.Observers.Toys;
using CodeBase.Logic.Scenes.Company.Presenters.Confetti;
using CodeBase.Logic.Scenes.Company.Presenters.Finish;
using CodeBase.Logic.Scenes.Company.Presenters.Music;
using CodeBase.Logic.Scenes.Company.Presenters.Toys;
using CodeBase.Logic.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Scenes.Company.Providers.Objects.Lines;
using CodeBase.Logic.Scenes.Company.Systems.Cameras;
using CodeBase.Logic.Scenes.Company.Systems.Debug;
using CodeBase.Logic.Scenes.Company.Systems.Finish;
using CodeBase.Logic.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Scenes.Company.Systems.Saver;
using CodeBase.Logic.Scenes.Company.Systems.Toys;
using CodeBase.UI.General.Factories.Windows.Pause;
using CodeBase.UI.General.Windows.Pause;
using CodeBase.UI.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Scenes.Company.Presenters.Windows;
using CodeBase.UI.Scenes.Company.Windows.Main;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Installers
{
    public class CompanySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFormulas();
            BindFactories();
            BindProviders();
            BindPresenters();
            BindSystems();
            BindDebug();
        }
        
        private void BindFormulas()
        {
            Container.BindInterfacesTo<ClickFormulas>().AsSingle();
            Container.BindInterfacesTo<RayFormulas>().AsSingle();
            Container.BindInterfacesTo<EdgeFormulas>().AsSingle();
            Container.BindInterfacesTo<CameraFormulas>().AsSingle();
        }

        private void BindFactories()
        {
            // Game - Objects
            Container.BindInterfacesTo<CompanyLevelFactory>().AsSingle();
            Container.BindInterfacesTo<ToyFactory>().AsSingle();
            Container.BindInterfacesTo<ToyShadowFactory>().AsSingle();
            Container.BindInterfacesTo<BabbleFactory>().AsSingle();
            Container.BindInterfacesTo<FinishLineFactory>().AsSingle();
            
            Container.BindFactory<FinishLineMediator, FinishLine, FinishLine.Factory>().AsSingle();
            
            // Game - Effects
            Container.BindInterfacesTo<ToySelectEffectFactory>().AsSingle();
            Container.BindInterfacesTo<ConfettiEffectFactory>().AsSingle();
            
            // UI - Elements
            Container.BindFactory<Slider, CanvasGroup, ToyRotatorElement, ToyRotatorElement.Factory>().AsSingle();
            
            // UI - Windows
            Container.BindInterfacesTo<CompanyMainWindowFactory>().AsSingle();
            Container.BindInterfacesTo<PauseWindowFactory>().AsSingle();
        }

        private void BindProviders()
        {
            // Objects
            Container.BindInterfacesTo<LevelProvider>().AsSingle();
            Container.BindInterfacesTo<WindowCanvasProvider>().AsSingle();
            Container.BindInterfacesTo<GameCanvasProvider>().AsSingle();
            Container.BindInterfacesTo<ToyProvider>().AsSingle();
            Container.BindInterfacesTo<FinishLineProvider>().AsSingle();
            
            // Data - ScriptableObjects
            Container.BindInterfacesTo<CompanyLevelsSettingProvider>().AsSingle();
            Container.BindInterfacesTo<CameraSettingsProvider>().AsSingle();
            
            // Data - Saves
            Container.BindInterfacesTo<CompanyLevelsSaveDataProvider>().AsSingle();
        }

        private void BindPresenters()
        {
            // Game - Effects
            Container.BindInterfacesTo<CompanySceneConfettiEffectPresenter>().AsSingle().NonLazy();
            
            // Sounds
            Container.BindInterfacesTo<CompanySceneMusicPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToySelectSoundPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyCollisionSoundPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FinishSoundPresenter>().AsSingle().NonLazy();
            
            // UI - Windows
            Container.Bind<CompanyMainWindowPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CompanyLoadingWindowPresenter>().AsSingle().NonLazy();
        }

        private void BindSystems()
        {            
            Container.BindInterfacesTo<LevelBorderSystem>().AsSingle();
            Container.BindInterfacesTo<CompanyLevelSizeSystem>().AsSingle();
            Container.BindInterfacesTo<CameraBorderSystem>().AsSingle();

            Container.BindInterfacesTo<CompanyLevelSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CompanyFinishSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CameraRenderSetup>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CompanyFinishLineSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CompanySceneSaver>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<CameraDisposer>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyDisposer>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FinishLineDisposer>().AsSingle().NonLazy();
            
            // Observers
            Container.BindInterfacesTo<CompanySceneReadyObserver>().AsSingle();
            Container.BindInterfacesTo<CompanyToyCountObserver>().AsSingle();
            Container.BindInterfacesTo<ToySelectObserver>().AsSingle();
            Container.BindInterfacesTo<ToyTowerBuildObserver>().AsSingle();
            Container.BindInterfacesTo<ToyCollisionObserver>().AsSingle();
            Container.BindInterfacesTo<ToyClickObserver>().AsSingle();
            Container.BindInterfacesTo<ToyMovementObserver>().AsSingle();
            Container.BindInterfacesTo<FinishObserver>().AsSingle();


            // Toy
            ToyStateMachineInstaller.Install(Container);

            Container.BindInterfacesTo<CompanyToySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CompanyToyDestroyer>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyShadowSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyOutlineSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyBuildEffectSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyBabbleSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ToyRotateAnimation>().AsSingle().NonLazy();
            
            CameraStateMachineInstaller.Install(Container);
            
            // UI - Windows
            Container.BindInterfacesTo<CompanyMainWindow>().AsSingle().NonLazy();
            Container.Bind<PauseWindow>().AsSingle().NonLazy();
        }
        
        private void BindDebug()
        {
            Container.BindInterfacesAndSelfTo<CompanyLevelDebug>().AsSingle().NonLazy();
        }
    }
}