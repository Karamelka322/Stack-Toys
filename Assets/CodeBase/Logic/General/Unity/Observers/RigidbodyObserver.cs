using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Unity.Observers
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyObserver : MonoBehaviour
    {
        [SerializeField, Required] 
        private Rigidbody _rigidbody;
        
        public BoolReactiveProperty IsSleeping { get; } = new();
        public ReactiveCollection<GameObject> Collisions { get; } = new();

        private void Update()
        {
            IsSleeping.Value = _rigidbody.IsSleeping();
        }

        private void OnCollisionEnter(Collision other)
        {
            Collisions.Add(other.gameObject);
        }

        private void OnCollisionExit(Collision other)
        {
            Collisions.Remove(other.gameObject);
        }
    }
}