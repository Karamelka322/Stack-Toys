using CodeBase.Logic.Services.Window;
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
            BindPresenters();
            BindFactories();
            BindSystems();
        }

        private void BindProviders()
        {
            Container.BindInterfacesTo<WindowCanvasProvider>().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesTo<MenuWindow>().AsSingle();
            Container.BindInterfacesTo<LevelsWindow>().AsSingle();
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<MenuWindowFactory>().AsSingle();
            Container.BindInterfacesTo<LevelsWindowFactory>().AsSingle();
        }

        private void BindPresenters()
        {
            Container.Bind<MenuWindowPresenter>().AsSingle().NonLazy();
        }
    }
}