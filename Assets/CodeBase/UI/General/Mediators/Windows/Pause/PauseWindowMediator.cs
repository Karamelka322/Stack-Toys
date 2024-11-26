using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.General.Mediators.Windows.Pause
{
    public class PauseWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Button _menuButton;
        
        [SerializeField, Required] 
        private Button _closeButton;
        
        public Button MenuButton => _menuButton;
        public Button CloseButton => _closeButton;
    }
}