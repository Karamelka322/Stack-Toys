using CodeBase.Data.Constants;
using UnityEngine;

namespace CodeBase.Data.ScriptableObjects.Cameras
{
    [CreateAssetMenu(menuName = AssetMenuConstants.ScriptableObjects + nameof(CameraSettings), fileName = nameof(CameraSettings))]
    public class CameraSettings : ScriptableObject
    {
        [SerializeField] 
        private float _scrollingSpeed = 1;
        
        public float ScrollingSpeed => _scrollingSpeed;
    }
}