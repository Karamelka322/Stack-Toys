using CodeBase.Data.Constants;
using CodeBase.Data.ScriptableObjects.Levels;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Models.Levels
{
    [CreateAssetMenu(menuName = AssetMenuConstants.ScriptableObjects + nameof(LevelsConfig), fileName = nameof(LevelsConfig))]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField, Required] 
        private LevelConfigData[] _levels;
        
        public LevelConfigData[] Levels => _levels;
    }
}