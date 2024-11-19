using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Load
{
    public interface ICompanySceneLoad
    {
        BoolReactiveProperty IsLoaded { get; }
    }
}