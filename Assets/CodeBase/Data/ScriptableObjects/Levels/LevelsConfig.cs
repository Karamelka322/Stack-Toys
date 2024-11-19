using CodeBase.Data.Constants;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Data.ScriptableObjects.Levels
{
    [CreateAssetMenu(menuName = AssetMenuConstants.ScriptableObjects + nameof(LevelsConfig), fileName = nameof(LevelsConfig))]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField, Required]
        private AssetReferenceGameObject _LevelPrefab;

        public AssetReferenceGameObject LevelPrefab => _LevelPrefab;
    }
}