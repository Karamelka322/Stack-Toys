using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Data.Models.Levels
{
    [Serializable]
    public class LevelConfigData
    {
        [SerializeField, Required]
        private AssetReferenceGameObject _levelAsset;
        
        [SerializeField]
        private float _height;
        
        [SerializeField, Required] 
        private AssetReferenceGameObject[] _toyAssets;
        
        public AssetReferenceGameObject LevelAsset => _levelAsset;
        public AssetReferenceGameObject[] ToyAssets => _toyAssets;
        public float Height => _height;
    }
}