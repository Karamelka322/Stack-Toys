using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Interfaces.General.Services.SceneLoad
{
    public interface ISceneLoadService
    {
        event Action<Scene> OnSceneReload;
        event Action<Scene> OnSceneLoading;
    
        void LoadScene(string sceneName);
        
        /// <summary>
        /// Закрывает и открывает текущую активную сцену
        /// </summary>
        UniTask ReloadSceneAsync(float delay);

        UniTask LoadSceneAsync(string sceneName, float delay);
    }
}