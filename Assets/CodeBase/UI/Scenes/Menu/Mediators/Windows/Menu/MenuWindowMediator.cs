using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenes.Menu.Mediators.Windows.Menu
{
    public class MenuWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Button _companyButton;
        
        [SerializeField, Required] 
        private Button _infinityModeButton;
        
        [SerializeField, Required] 
        private Button _closedInfinityModeButton;

        [SerializeField, Required] 
        private TextMeshProUGUI _levelsCounter;

        public Button CompanyButton => _companyButton;
        public Button InfinityModeButton => _infinityModeButton;
        public Button ClosedInfinityModeButton => _closedInfinityModeButton;
        public TextMeshProUGUI LevelsCounter => _levelsCounter;
    }
}