using UniRx;

namespace CodeBase.Logic.Scenes.Menu.Systems
{
    public interface IMenuSceneEnvironmentLoader
    {
        BoolReactiveProperty IsLoaded { get; }
    }
}