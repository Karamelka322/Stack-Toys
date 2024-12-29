using CodeBase.Logic.General.Factories.Audio;
using CodeBase.Logic.General.Providers.Data.Saves;
using CodeBase.Logic.General.Providers.Data.ScriptableObjects.Audio;
using CodeBase.Logic.General.Services.Assets;
using CodeBase.Logic.General.Services.Audio;
using CodeBase.Logic.General.Services.Debug;
using CodeBase.Logic.General.Services.Files;
using CodeBase.Logic.General.Services.Input;
using CodeBase.Logic.General.Services.SaveLoad;
using CodeBase.Logic.General.Services.SaveLoad.Formatters;
using CodeBase.Logic.General.Services.SceneLoad;
using CodeBase.Logic.General.Services.Windows;
using CodeBase.UI.General.Factories.Canvases;
using CodeBase.UI.General.Factories.Windows.Loading;
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
            BindServices();
            BindPresenters();
        }

        private void BindProviders()
        {
            Container.BindInterfacesTo<PlayerSaveDataProvider>().AsSingle();
            Container.BindInterfacesTo<AudioSettingsProvider>().AsSingle();
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<LoadingWindowFactory>().AsSingle();
            Container.BindInterfacesTo<CanvasFactory>().AsSingle();
            Container.BindInterfacesTo<AudioFactory>().AsSingle();
        }

        private void BindWindows()
        {
            Container.BindInterfacesTo<LoadingWindow>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<SceneLoadService>().AsSingle();
            Container.BindInterfacesTo<AssetService>().AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.BindInterfacesTo<FileService>().AsSingle();
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
            Container.BindInterfacesTo<BinaryFormatter>().AsSingle();
            Container.BindInterfacesTo<WindowService>().AsSingle();
            Container.BindInterfacesTo<AudioService>().AsSingle();
            Container.BindInterfacesTo<DebugService>().AsSingle();
        }

        private void BindPresenters()
        {
            Container.Bind<StartLoadingScreenPresenter>().AsSingle().NonLazy();
        }
    }
}