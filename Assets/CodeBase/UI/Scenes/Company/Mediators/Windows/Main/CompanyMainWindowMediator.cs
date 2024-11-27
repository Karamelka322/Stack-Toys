using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenes.Company.Mediators.Windows.Main
{
    public class CompanyMainWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Slider _slider;

        [SerializeField, Required] 
        private Button _pauseButton;
        
        [SerializeField, Required] 
        private TextMeshProUGUI _toyCounter;
        
        public Slider Slider => _slider;
        public Button PauseButton => _pauseButton;
        public TextMeshProUGUI ToyCounter => _toyCounter;
    }
}