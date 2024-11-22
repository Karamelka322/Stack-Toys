using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load
{
    public interface ICompanySceneLoad
    {
        BoolReactiveProperty IsLoaded { get; }
    }
}