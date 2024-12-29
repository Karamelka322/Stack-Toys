using UnityEngine;

namespace CodeBase.Logic.Scenes.Infinity.Unity.Toys
{
    public class ToyChoicerMediator : MonoBehaviour
    {
        [SerializeField] private Transform _toySlot1;
        [SerializeField] private Transform _toySlot2;

        public Transform ToySlot1 => _toySlot1;
        public Transform ToySlot2 => _toySlot2;
    }
}