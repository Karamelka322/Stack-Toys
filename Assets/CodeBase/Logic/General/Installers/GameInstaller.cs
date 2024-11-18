using CodeBase.Logic.Services;
using CodeBase.Logic.Services.Assets;
using CodeBase.Logic.Services.SceneLoad;
using CodeBase.Logic.Services.Window;
using CodeBase.UI.General.Factories.Canvases;
using CodeBase.UI.General.Factories.Loading;
using CodeBase.UI.General.Presenters.Loading;
using CodeBase.UI.General.Windows.Loading;
using Zenject;

namespace CodeBase.Logic.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
            BindWindows();
            BindPresenters();
            BindServices();
        }

        private void BindPresenters()
        {
            Container.Bind<StartLoadingScreenPresenter>().AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<LoadingWindowFactory>().AsSingle();
            Container.BindInterfacesTo<CanvasFactory>().AsSingle();
        }

        private void BindWindows()
        {
            Container.BindInterfacesTo<LoadingWindow>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<SceneLoadService>().AsSingle();
            Container.BindInterfacesTo<AssetServices>().AsSingle();
            Container.BindInterfacesTo<WindowService>().AsSingle();
        }
    }
}