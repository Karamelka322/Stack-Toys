using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Data.ScriptableObjects.Levels
{
    [Serializable]
    public class LevelConfigData
    {
        [SerializeField, Required]
        private AssetReferenceGameObject _levelAsset;

        [SerializeField, Required] 
        private AssetReferenceGameObject[] _toyAssets;
        
        public AssetReferenceGameObject LevelAsset => _levelAsset;
        public AssetReferenceGameObject[] ToyAssets => _toyAssets;
    }
}