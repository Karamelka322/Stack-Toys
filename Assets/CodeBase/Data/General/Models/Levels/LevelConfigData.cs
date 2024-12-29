using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Data.General.Models.Levels
{
    [Serializable]
    public class LevelConfigData
    {
        [SerializeField, Required]
        private AssetReferenceGameObject _environmentAsset;
        
        [SerializeField]
        private float _height;
        
        [SerializeField, Required] 
        private AssetReferenceGameObject[] _toyAssets;
        
        public AssetReferenceGameObject EnvironmentAsset => _environmentAsset;
        public AssetReferenceGameObject[] ToyAssets => _toyAssets;
        public float Height => _height;
    }
}