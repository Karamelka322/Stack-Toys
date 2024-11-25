using Sirenix.OdinInspector;
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

        public Transform LevelsParent => _levelsParent;
        public Button BackButton => _backButton;
    }
}