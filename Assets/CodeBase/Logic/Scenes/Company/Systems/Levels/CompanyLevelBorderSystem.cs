using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.Interfaces.General.Formulas;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Scenes.Company.Systems.Levels
{
    public class CompanyLevelBorderSystem : BaseLevelBorderSystem
    {
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly ICompanyLevelsSettingProvider _companyLevelsSettingProvider;

        public CompanyLevelBorderSystem(ILevelProvider levelProvider, IRayFormulas rayFormulas,
            IEdgeFormulas edgeFormulas, ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider,
            ICompanyLevelsSettingProvider companyLevelsSettingProvider) : base(levelProvider, rayFormulas, edgeFormulas)
        {
            _companyLevelsSettingProvider = companyLevelsSettingProvider;
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
        }
        
        public override async UniTask<float> GetHeightAsync()
        {
            var currentLevel = _companyLevelsSaveDataProvider.GetCurrentLevel();
            return await _companyLevelsSettingProvider.GetLevelHeightAsync(currentLevel);
        }
    }
}