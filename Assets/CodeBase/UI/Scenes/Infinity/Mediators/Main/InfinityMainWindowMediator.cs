using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenes.Infinity.Mediators.Main
{
    public class InfinityMainWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Slider _slider;
        
        [SerializeField, Required] 
        private CanvasGroup _sliderCanvasGroup;

        [SerializeField, Required] 
        private Button _pauseButton;
        
        public Slider Slider => _slider;
        public Button PauseButton => _pauseButton;
        public CanvasGroup SliderCanvasGroup => _sliderCanvasGroup;
    }
}