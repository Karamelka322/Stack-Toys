using CodeBase.Logic.General.Unity.Toys;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.CodeBase
{
    [CustomEditor(typeof(ToyMediator))]
    public class ToyMediatorEditor : OdinEditor
    {
        private void OnSceneGUI()
        {
            var level = target as ToyMediator;
            
            if (level == null)
            {
                return;
            }
            
            DrawSize(level);
        }

        private void DrawSize(ToyMediator toy)
        {
            Handles.color = Color.yellow;
            
            Handles.DrawWireDisc(toy.transform.position, Vector3.forward, toy.BabbleSize);
        }
    }
}