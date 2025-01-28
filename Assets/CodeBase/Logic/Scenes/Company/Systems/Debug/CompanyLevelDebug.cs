using System.ComponentModel;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Services.Debug;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.Debug;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace CodeBase.Logic.Scenes.Company.Systems.Debug
{
    public class CompanyLevelDebug : BaseDebugOptions
    {
        private const string CategoryName = "CompanyLevel";
        
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly ISceneLoadService _sceneLoadService;

        public CompanyLevelDebug(IDebugService debugService, ISceneReadyObserver sceneReady, ISceneLoadService sceneLoadService,
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider) : base(debugService, sceneReady)
        {
            _sceneLoadService = sceneLoadService;
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
        }
        
#if !DISABLE_SRDEBUGGER

        [UsedImplicitly]
        [Category(CategoryName)]
        [SROptions.NumberRange(1, CompanyConstants.NumberOfLevels)]
        public int Level
        {
            get => _companyLevelsSaveDataProvider.GetCurrentLevel() + 1;
            set
            {
                _companyLevelsSaveDataProvider.SetLastOpenedLevel(value - 1);
                _companyLevelsSaveDataProvider.SetCurrentLevel(value - 1);

                Reload();
            }
        }

        [UsedImplicitly]
        [Category(CategoryName)]
        public void Reload()
        {
            _sceneLoadService.ReloadSceneAsync(1f).Forget();
        }

#endif

    }
}