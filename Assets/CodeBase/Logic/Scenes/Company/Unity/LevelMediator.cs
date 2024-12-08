using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Unity
{
    public class LevelMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private GameObject _floor;
        
        [SerializeField, Required] 
        private Transform _originPoint;
        
        [SerializeField, Required] 
        private float _width;

        public GameObject Floor => _floor;
        public Transform OriginPoint => _originPoint;
        public float Width => _width;
    }
}