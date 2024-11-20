using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenes.Company.Windows.Main
{
    public class CompanyMainWindowMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Slider _slider;

        public Slider Slider => _slider;
    }
}