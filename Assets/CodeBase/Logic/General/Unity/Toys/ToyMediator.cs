using CodeBase.Logic.General.Unity.Observers;
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
        private MeshRenderer _meshRenderer;

        [Space, SerializeField, Required] 
        private Collider[] _colliders;

        public Rigidbody Rigidbody => _rigidbody;
        public RigidbodyObserver RigidbodyObserver => _rigidbodyObserver;
        public Collider[] Colliders => _colliders;
        public MeshRenderer MeshRenderer => _meshRenderer;
    }
}