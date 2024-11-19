using CodeBase.Logic.Scenes.Company.Unity;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    [CustomEditor(typeof(Level))]
    public class LevelEditor : OdinEditor
    {
        private void OnSceneGUI()
        {
            var level = target as Level;
            
            if (level == null || level.CameraStartPoint == null || level.CameraEndPoint == null)
            {
                return;
            }
            
            Handles.color = Color.red;  // Устанавливаем цвет линии
            Handles.DrawLine(level.CameraStartPoint.position, level.CameraEndPoint.position);
        }
    }
}