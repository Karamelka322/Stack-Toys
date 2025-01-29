using CodeBase.Logic.Scenes.Company.Unity;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.CodeBase
{
    [CustomEditor(typeof(LevelMediator))]
    public class LevelMediatorEditor : OdinEditor
    {
        private const float _height = 10f;
        private const float _width = 5.5f;
        
        private void OnSceneGUI()
        {
            var level = target as LevelMediator;
            
            if (level == null)
            {
                return;
            }
            
            TrySetLevelBorderGizmo(level);
        }

        private void TrySetLevelBorderGizmo(LevelMediator level)
        {
            if (level.OriginPoint == null)
            {
                return;
            }
            
            var bottomLeft = level.OriginPoint.position - level.OriginPoint.right * _width / 2f;
            var bottomRight = level.OriginPoint.position + level.OriginPoint.right * _width / 2f;
            
            var topLeft = bottomLeft + Vector3.up * _height;
            var topRight = bottomRight + Vector3.up * _height;
            
            Handles.color = Color.yellow;
            
            Handles.DrawLine(bottomLeft, bottomRight);
            Handles.DrawLine(topLeft, topRight);
            
            Handles.DrawLine(bottomLeft, topLeft);
            Handles.DrawLine(bottomRight, topRight);
        }
    }
}