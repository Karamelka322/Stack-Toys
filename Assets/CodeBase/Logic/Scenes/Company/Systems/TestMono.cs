using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems
{
    public class TestMono : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;
        
        [Button]
        private void Test()
        {
            UnityEngine.Debug.Log(_meshRenderer.bounds.size);
        }
    }
}