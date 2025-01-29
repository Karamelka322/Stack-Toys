using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.General.Unity.Finish
{
    public class FinishLineMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private TextMeshPro _finish;
        
        [SerializeField, Required] 
        private TextMeshPro _height;
        
        [SerializeField, Required] 
        private RectTransform _heightRectTransform;

        [SerializeField, Required] 
        private SpriteRenderer _line;
        
        public TextMeshPro Finish => _finish;
        public TextMeshPro Height => _height;
        public SpriteRenderer Line => _line;
        public RectTransform HeightRectTransform => _heightRectTransform;
    }
}