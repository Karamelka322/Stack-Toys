using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.General.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}