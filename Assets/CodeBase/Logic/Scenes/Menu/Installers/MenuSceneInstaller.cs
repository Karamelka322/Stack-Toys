using CodeBase.Logic.General.Providers.Data.Saves;
using CodeBase.Logic.General.Providers.Data.ScriptableObjects.Levels;
using CodeBase.Logic.General.Providers.Objects.Canvases;
using CodeBase.Logic.Scenes.Menu.Observers.Ready;
using CodeBase.Logic.Scenes.Menu.Systems;
using CodeBase.UI.Scenes.Menu.Factories.Levels;
using CodeBase.UI.Scenes.Menu.Factories.Menu;
using CodeBase.UI.Scenes.Menu.Presenters.Menu;
using CodeBase.UI.Scenes.Menu.Windows.Levels;
using CodeBase.UI.Scenes.Menu.Windows.Menu;
using Zenject;

namespace CodeBase.Logic.Scenes.Menu.Installers
{
    public class MenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindProviders();
            BindFactories();
            BindSystems();
            BindPresenters();
            BindObservers();
        }

        private void BindProviders()
        {
            // Objects
            Container.BindInterfacesTo<WindowCanvasProvider>().AsSingle();
            
            // Data
            Container.BindInterfacesTo<CompanyLevelsSaveDataProvider>().AsSingle();
            Container.BindInterfacesTo<CompanyLevelsSettingProvider>().AsSingle();
        }

        private void BindObservers()
        {
            Container.BindInterfacesTo<MenuSceneReadyObserver>().AsSingle();
        }

        private void BindFactories()
        {
            // UI - Windows
            Container.BindInterfacesTo<MenuWindowFactory>().AsSingle();
            Container.BindInterfacesTo<LevelsWindowFactory>().AsSingle();
            
            // UI - Elements
            Container.BindInterfacesTo<MenuLevelElementFactory>().AsSingle();
            
            Container.BindFactory<MenuLevelElementMediator, int, OpenedMenuLevelElement, OpenedMenuLevelElement.Factory>();
            Container.BindFactory<MenuLevelElementMediator, int, ClosedMenuLevelElement, ClosedMenuLevelElement.Factory>();
            Container.BindFactory<MenuLevelElementMediator, int, CompletedMenuLevelElement, CompletedMenuLevelElement.Factory>();
        }

        private void BindSystems()
        {
            Container.BindInterfacesTo<MenuWindow>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LevelsWindow>().AsSingle().NonLazy();
            Container.BindInterfacesTo<MenuSceneEnvironmentLoader>().AsSingle().NonLazy();
        }

        private void BindPresenters()
        {
            Container.Bind<MenuWindowPresenter>().AsSingle().NonLazy();
            Container.Bind<MenuLoadingScreenPresenter>().AsSingle().NonLazy();
        }
    }
}