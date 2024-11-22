using CodeBase.Logic.General.Unity.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Toys;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Logic.General.Unity.Toys
{
    public class ToyMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Rigidbody _rigidbody;
        
        [SerializeField, Required]
        private RigidbodyObserver _rigidbodyObserver;

        [SerializeField, Required] 
        private Collider _collider;

        [SerializeField, Required] 
        private MeshRenderer _meshRenderer;
        
        public Rigidbody Rigidbody => _rigidbody;
        public RigidbodyObserver RigidbodyObserver => _rigidbodyObserver;
        public Collider Collider => _collider;
        public MeshRenderer MeshRenderer => _meshRenderer;
    }
}