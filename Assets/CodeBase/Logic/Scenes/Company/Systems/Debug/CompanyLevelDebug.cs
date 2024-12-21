using System.ComponentModel;
using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.Logic.Scenes.Company.Systems.Ready;
using JetBrains.Annotations;

namespace CodeBase.CodeBase.Logic.Services.Debug
{
    public class CompanyLevelDebug : BaseDebugOptions
    {
        private const string CategoryName = "CompanyLevel";
        
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ICompanySceneUnload _companySceneUnload;

        public CompanyLevelDebug(IDebugService debugService, ISceneReadyObserver sceneReady, ISceneLoadService sceneLoadService,
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider, ICompanySceneUnload companySceneUnload) : base(debugService, sceneReady)
        {
            _companySceneUnload = companySceneUnload;
            _sceneLoadService = sceneLoadService;
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
        }

        [UsedImplicitly]
        [Category(CategoryName)]
        [SROptions.NumberRange(1, CompanyConstants.NumberOfLevels)]
        public int Level
        {
            get => _companyLevelsSaveDataProvider.GetCurrentLevel() + 1;
            set
            {
                _companyLevelsSaveDataProvider.SetTargetLevel(value - 1);
                _companyLevelsSaveDataProvider.SetCurrentLevel(value - 1);

                Reload();
            }
        }

        [UsedImplicitly]
        [Category(CategoryName)]
        public void Reload()
        {
            _companySceneUnload.Unload();
            _sceneLoadService.ReloadScene();
        }
    }
}