using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenes.Menu.Mediators.Windows.Levels
{
    public class LevelsWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Button _backButton;
        
        [SerializeField, Required] 
        private Transform _levelsParent;
        
        [SerializeField, Required] 
        private TextMeshProUGUI _title;

        public Transform LevelsParent => _levelsParent;
        public Button BackButton => _backButton;
        public TextMeshProUGUI Title => _title;
    }
}