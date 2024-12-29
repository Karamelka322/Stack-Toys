using CodeBase.Data.General.Constants;
using CodeBase.Data.General.Models.Levels;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Scenes.Company.ScriptableObjects.Levels
{
    [CreateAssetMenu(menuName = AssetMenuConstants.ScriptableObjects + nameof(CompanyLevelsSettings), fileName = nameof(CompanyLevelsSettings))]
    public class CompanyLevelsSettings : ScriptableObject
    {
        [SerializeField, Required, ListDrawerSettings(NumberOfItemsPerPage = 1)] 
        private LevelConfigData[] _levels;
        
        public LevelConfigData[] Levels => _levels;
    }
}