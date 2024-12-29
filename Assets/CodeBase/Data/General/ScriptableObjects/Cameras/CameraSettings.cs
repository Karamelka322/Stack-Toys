using CodeBase.Data.General.Constants;
using UnityEngine;

namespace CodeBase.Data.General.ScriptableObjects.Cameras
{
    [CreateAssetMenu(menuName = AssetMenuConstants.ScriptableObjects + nameof(CameraSettings), fileName = nameof(CameraSettings))]
    public class CameraSettings : ScriptableObject
    {
        [SerializeField] 
        private float _scrollingSpeed = 1;
        
        [SerializeField] 
        private Vector3 _offset;
        
        public float ScrollingSpeed => _scrollingSpeed;
        public Vector3 Offset => _offset;
    }
}