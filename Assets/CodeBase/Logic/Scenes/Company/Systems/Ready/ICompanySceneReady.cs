using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Ready
{
    public interface ICompanySceneReady
    {
        BoolReactiveProperty IsReady { get; }
    }
}