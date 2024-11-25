using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class MenuLevelElementMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private TextMeshProUGUI _label;
        
        [SerializeField, Required] 
        private Button _button;

        public Button Button => _button;
        public TextMeshProUGUI Label => _label;
    }
}