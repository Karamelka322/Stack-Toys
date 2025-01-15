using System;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.General.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        public event Action<Scene> OnSceneLoading;
        public event Action<Scene> OnSceneReload;
        
        public void LoadScene(string sceneName)
        {
            OnSceneLoading?.Invoke(SceneManager.GetSceneByName(sceneName));
            
            SceneManager.LoadScene(sceneName);
        }
        
        public async UniTask LoadSceneAsync(string sceneName, float delay)
        {
            OnSceneLoading?.Invoke(SceneManager.GetSceneByName(sceneName));
            
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            
            SceneManager.LoadScene(sceneName);
        }

        public async UniTask ReloadSceneAsync(float delay)
        {
            OnSceneReload?.Invoke(SceneManager.GetActiveScene());

            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}