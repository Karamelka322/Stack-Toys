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
            Container.BindInterfacesTo<BootstrapSceneReady>().AsSingle().NonLazy();
        }
    }
}