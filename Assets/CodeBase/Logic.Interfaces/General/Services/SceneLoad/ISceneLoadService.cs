namespace CodeBase.Logic.Interfaces.General.Services.SceneLoad
{
    public interface ISceneLoadService
    {
        void LoadScene(string sceneName);
        
        /// <summary>
        /// Закрывает и открывает текущую активную сцену
        /// </summary>
        void ReloadScene();
    }
}