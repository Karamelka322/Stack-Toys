using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;

namespace CodeBase.Logic.General.Providers.Data.Saves
{
    public class CompanyLevelsSaveDataProvider : ICompanyLevelsSaveDataProvider
    {
        private readonly IPlayerSaveDataProvider _playerSaveDataProvider;

        public CompanyLevelsSaveDataProvider(IPlayerSaveDataProvider playerSaveDataProvider)
        {
            _playerSaveDataProvider = playerSaveDataProvider;
        }
        
        
    }
}