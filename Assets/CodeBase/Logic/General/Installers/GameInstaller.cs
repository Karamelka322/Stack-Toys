using CodeBase.Logic.General.Providers.Data.Saves;
using CodeBase.Logic.General.Services.Assets;
using CodeBase.Logic.General.Services.Files;
using CodeBase.Logic.General.Services.Input;
using CodeBase.Logic.General.Services.SaveLoad;
using CodeBase.Logic.General.Services.SceneLoad;
using CodeBase.UI.General.Factories.Canvases;
using CodeBase.UI.General.Factories.Loading;
using CodeBase.UI.General.Presenters.Loading;
using CodeBase.UI.General.Windows.Loading;
using Zenject;

namespace CodeBase.Logic.General.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindProviders();
            BindFactories();
            BindWindows();
            BindPresenters();
            BindServices();
        }

        private void BindProviders()
        {
            Container.BindInterfacesTo<PlayerSaveDataProvider>().AsSingle();
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
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.BindInterfacesTo<FileService>().AsSingle();
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
        }
    }
}