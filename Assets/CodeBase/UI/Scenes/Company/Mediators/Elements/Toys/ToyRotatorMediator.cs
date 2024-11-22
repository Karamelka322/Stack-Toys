using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.UI.Scenes.Company.Mediators.Elements.Toys
{
    public class ToyRotatorMediator : MonoBehaviour
    {
        [SerializeField, Required]
        private RectTransform _rectTransform;
        
        [SerializeField, Required] 
        private RectTransform _bigStick;

        [SerializeField, Required] 
        private RectTransform _smallStick;

        public RectTransform BigStick => _bigStick;
        public RectTransform SmallStick => _smallStick;
        public RectTransform RectTransform => _rectTransform;
    }
}