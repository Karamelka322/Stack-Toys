using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.General.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);

#if UNITY_EDITOR
            
            DynamicGI.UpdateEnvironment();
            
#endif
            
        }
    }
}