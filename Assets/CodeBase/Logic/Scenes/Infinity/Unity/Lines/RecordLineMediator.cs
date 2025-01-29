using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Infinity.Unity.Lines
{
    public class RecordLineMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private TextMeshPro _title;
        
        [SerializeField, Required] 
        private TextMeshPro _height;
        
        [SerializeField, Required] 
        private RectTransform _heightRectTransform;
        
        [SerializeField, Required] 
        private SpriteRenderer _line;
        
        public TextMeshPro Title => _title;
        public TextMeshPro Height => _height;
        public SpriteRenderer Line => _line;
        public RectTransform HeightRectTransform => _heightRectTransform;
    }
}