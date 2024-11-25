using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenes.Company.Mediators.Windows.Finish
{
    public class CompanyFinishWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Button _nextLevelButton;

        public Button NextLevelButton => _nextLevelButton;
    }
}