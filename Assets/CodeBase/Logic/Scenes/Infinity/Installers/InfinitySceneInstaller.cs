using CodeBase.Logic.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Scenes.Infinity.Factories.Levels;
using CodeBase.Logic.Scenes.Infinity.Observers.Ready;
using CodeBase.Logic.Scenes.Infinity.Systems.Levels;
using CodeBase.UI.Scenes.Company.Presenters.Windows;
using Zenject;

namespace CodeBase.Logic.Scenes.Infinity.Installers
{
    public class InfinitySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindProviders();
            BindFactories();
            BindPresenters();
            BindSystems();
            BindObservers();
        }

        private void BindProviders()
        {
            Container.BindInterfacesTo<LevelProvider>().AsSingle();
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<InfinityLevelFactory>().AsSingle();
        }

        private void BindPresenters()
        {
            Container.BindInterfacesTo<CompanyLoadingWindowPresenter>().AsSingle().NonLazy();
        }

        private void BindObservers()
        {
            Container.BindInterfacesTo<InfinitySceneReadyObserver>().AsSingle().NonLazy();
        }

        private void BindSystems()
        {
            Container.BindInterfacesTo<InfinityLevelSpawner>().AsSingle().NonLazy();
        }
    }
}