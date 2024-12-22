using UnityEngine;

namespace CodeBase.UI.General.Mediators.Windows.Loading
{
    public class LoadingWindowMediator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public Animator Animator => _animator;
    }
}