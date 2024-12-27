using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace CodeBase.CodeBase.Editor.Tools
{
    /// <summary>
    /// Инструмент для отчистки игровых данных
    /// </summary>
    [UsedImplicitly]
    public class ClearDataTool
    {
        [MenuItem("Game/Clear Data/All Game Saves")]
        private static void Clear()
        {
            ClearDirectory();
            ClearPlayerPrefs();
            
            Debug.Log("Все игровые сохранения удалены.");
        }
        
        private static void ClearDirectory()
        {
            var files = Directory.GetFiles(Application.persistentDataPath);
            
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        private static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}