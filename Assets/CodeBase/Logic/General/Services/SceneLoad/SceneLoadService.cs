using CodeBase.Logic.Interfaces.Services.SceneLoad;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Services.SceneLoad
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