using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenes.Menu.Mediators.Menu
{
    public class MenuWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Button _companyButton;
        
        [SerializeField, Required] 
        private Button _infinityModeButton;

        public Button CompanyButton => _companyButton;
        public Button InfinityModeButton => _infinityModeButton;
    }
}