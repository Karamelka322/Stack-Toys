using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Cameras;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine;

namespace CodeBase.Logic.Scenes.Company.Systems.Load
{
    public class CompanySceneUnload : ICompanySceneUnload
    {
        private readonly ICameraStateMachine _cameraStateMachine;
        private readonly IToyProvider _toyProvider;

        public CompanySceneUnload(ICameraStateMachine cameraStateMachine, IToyProvider toyProvider)
        {
            _toyProvider = toyProvider;
            _cameraStateMachine = cameraStateMachine;
        }

        public void Unload()
        {
            foreach (var toy in _toyProvider.Toys)
            {
                toy.Item2.Reset();
            }
            
            _toyProvider.Dispose();
            _cameraStateMachine.Dispose();
        }
    }
}