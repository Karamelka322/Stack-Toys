using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.General.Mediators.Windows.Pause
{
    public class PauseWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private TextMeshProUGUI _musicLabel;
        
        [SerializeField, Required] 
        private TextMeshProUGUI _soundsLabel;
        
        [SerializeField, Required] 
        private TextMeshProUGUI _localizationLabel;

        [Space, SerializeField, Required] 
        private Button _menuButton;
        
        [SerializeField, Required] 
        private Button _closeButton;
        
        [SerializeField, Required] 
        private Button _adsButton;
        
        [SerializeField, Required] 
        private Button _languageButton;

        [Space, SerializeField, Required] 
        private Image _languageImage;
        
        [Space, SerializeField, Required] 
        private Toggle _musicVolumeToggle;
        
        [SerializeField, Required] 
        private Toggle _soundVolumeToggle;
    
        public TextMeshProUGUI MusicLabel => _musicLabel;
        public TextMeshProUGUI SoundsLabel => _soundsLabel;
        public TextMeshProUGUI LocalizationLabel => _localizationLabel;

        public Button MenuButton => _menuButton;
        public Button CloseButton => _closeButton;
        public Button AdsButton => _adsButton;
        public Button LanguageButton => _languageButton;

        public Toggle MusicVolumeToggle => _musicVolumeToggle;
        public Toggle SoundVolumeToggle => _soundVolumeToggle;

        public Image LanguageImage => _languageImage;
    }
}