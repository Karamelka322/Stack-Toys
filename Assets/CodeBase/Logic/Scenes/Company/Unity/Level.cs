using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Unity
{
    public class Level : MonoBehaviour
    {
        [SerializeField, Required] 
        private Transform _cameraPoint;

        public Transform CameraPoint => _cameraPoint;
    }
}