using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Infinity.Unity.Lines
{
    public class RecordLineMediator : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshPro _title;
        
        [SerializeField] 
        private TextMeshPro _height;

        [SerializeField] 
        private TextMeshPro _line;
        
        public TextMeshPro Title => _title;
        public TextMeshPro Height => _height;
        public TextMeshPro Line => _line;
    }
}