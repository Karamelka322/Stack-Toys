using CodeBase.Logic.General.Providers.Data.Saves;
using CodeBase.Logic.Scenes.Bootstrap.Systems;
using CodeBase.Logic.Scenes.Bootstrap.Systems.Ready;
using Zenject;

namespace CodeBase.Logic.Scenes.Bootstrap.Installers
{
    public class BootstrapSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSystems();
        }

        private void BindSystems()
        {
            Container.BindInterfacesTo<CompanyLevelsSaveDataProvider>().AsSingle();
            Container.BindInterfacesTo<BootstrapSceneReadyObserver>().AsSingle();
            
            Container.BindInterfacesTo<OpenFirstSceneSystem>().AsSingle().NonLazy();
        }
    }
}