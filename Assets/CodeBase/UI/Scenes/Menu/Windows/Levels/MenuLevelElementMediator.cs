using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class MenuLevelElementMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Button _button;

        public Button Button => _button;
    }
}