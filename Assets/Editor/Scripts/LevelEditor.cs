using CodeBase.Logic.Scenes.Company.Unity;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    [CustomEditor(typeof(LevelMediator))]
    public class LevelEditor : OdinEditor
    {
        private void OnSceneGUI()
        {
            var level = target as LevelMediator;
            
            if (level == null || level.CameraStartPoint == null || level.CameraEndPoint == null)
            {
                return;
            }
            
            Handles.color = Color.red;  // Устанавливаем цвет линии
            Handles.DrawLine(level.CameraStartPoint.position, level.CameraEndPoint.position);
        }
    }
}