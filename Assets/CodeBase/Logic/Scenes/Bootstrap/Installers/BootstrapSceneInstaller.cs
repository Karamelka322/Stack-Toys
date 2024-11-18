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
            // Container.Bind<>()
        }
    }
}