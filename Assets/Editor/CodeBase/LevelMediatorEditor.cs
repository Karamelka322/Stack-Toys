using CodeBase.Logic.Scenes.Company.Unity;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.CodeBase
{
    [CustomEditor(typeof(LevelMediator))]
    public class LevelMediatorEditor : OdinEditor
    {
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

            var bottomLeft = level.OriginPoint.position - level.OriginPoint.right * level.Width / 2f;
            var bottomRight = level.OriginPoint.position + level.OriginPoint.right * level.Width / 2f;
            
            var topLeft = bottomLeft + level.OriginPoint.up * level.Height;
            var topRight = bottomRight + level.OriginPoint.up * level.Height;
            
            Handles.color = Color.yellow;
            
            Handles.DrawLine(bottomLeft, bottomRight);
            Handles.DrawLine(topLeft, topRight);
            
            Handles.DrawLine(bottomLeft, topLeft);
            Handles.DrawLine(bottomRight, topRight);
        }
    }
}