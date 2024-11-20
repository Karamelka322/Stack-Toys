using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Unity
{
    public class LevelMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Transform _cameraStartPoint;
        
        [SerializeField, Required] 
        private Transform _cameraEndPoint;

        [SerializeField, Required] 
        private Transform _toyPoint;

        public Transform CameraStartPoint => _cameraStartPoint;
        public Transform CameraEndPoint => _cameraEndPoint;
        public Transform ToyPoint => _toyPoint;
    }
}