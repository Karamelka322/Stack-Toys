using System;
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

        [SerializeField, Required] 
        private GameObject _floor;
        
        [SerializeField, Required] 
        private Transform _originPoint;
        
        [SerializeField, Required] 
        private float _height;
        
        [SerializeField, Required] 
        private float _width;

        public Transform CameraStartPoint => _cameraStartPoint;
        public Transform CameraEndPoint => _cameraEndPoint;
        public Transform ToyPoint => _toyPoint;
        public GameObject Floor => _floor;
        public Transform OriginPoint => _originPoint;
        public float Height => _height;
        public float Width => _width;
    }
}