using CodeBase.UI.Scenes.Bootstrap.Presenters;
using CodeBase.UI.Windows.Loading;
using Zenject;

namespace CodeBase.Logic.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
            BindServices();
            BindPresenters();
        }

        private void BindPresenters()
        {
            Container.Bind<StartLoadingScreenPresenter>().AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<LoadingWindowFactory>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<LoadingWindow>().AsSingle();
        }
    }
}