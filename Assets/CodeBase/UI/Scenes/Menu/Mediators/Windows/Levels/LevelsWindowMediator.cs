using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenes.Menu.Mediators.Windows.Levels
{
    public class LevelsWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Button _openNextSceneButton;

        public Button OpenNextSceneButton => _openNextSceneButton;
    }
}