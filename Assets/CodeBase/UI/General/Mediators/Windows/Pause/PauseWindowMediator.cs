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
        
        [SerializeField, Required] 
        private Button _adsButton;
        
        [SerializeField, Required] 
        private Toggle _musicVolumeToggle;
        
        [SerializeField, Required] 
        private Toggle _soundVolumeToggle;
        
        public Button MenuButton => _menuButton;
        public Button CloseButton => _closeButton;
        public Button AdsButton => _adsButton;
        public Toggle MusicVolumeToggle => _musicVolumeToggle;
        public Toggle SoundVolumeToggle => _soundVolumeToggle;
    }
}