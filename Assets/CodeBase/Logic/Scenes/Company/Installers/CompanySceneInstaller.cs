using CodeBase.Data.ScriptableObjects.Levels;
using CodeBase.Logic.Scenes.Company.Systems.Load;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Installers
{
    public class CompanySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindProviders();
            BindSystems();
        }
        
        private void BindProviders()
        {
            // ScriptableObjects
            Container.BindInterfacesTo<LevelsConfigProvider>().AsSingle();
        }
        
        private void BindSystems()
        {
            Container.BindInterfacesTo<CompanySceneLoad>().AsSingle().NonLazy();
        }
    }
}