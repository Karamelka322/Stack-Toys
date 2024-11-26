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
        
        public TextMeshPro Finish => _finish;
        public TextMeshPro Height => _height;
    }
}