using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Mediators.Windows.Levels
{
    public class LevelsWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Transform _levelsParent;

        public Transform LevelsParent => _levelsParent;
    }
}