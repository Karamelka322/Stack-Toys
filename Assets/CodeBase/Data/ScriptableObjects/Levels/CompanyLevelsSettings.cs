using CodeBase.Data.Constants;
using CodeBase.Data.Models.Levels;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.ScriptableObjects.Levels
{
    [CreateAssetMenu(menuName = AssetMenuConstants.ScriptableObjects + nameof(CompanyLevelsSettings), fileName = nameof(CompanyLevelsSettings))]
    public class CompanyLevelsSettings : ScriptableObject
    {
        [SerializeField, Required] 
        private LevelConfigData[] _levels;
        
        public LevelConfigData[] Levels => _levels;
    }
}