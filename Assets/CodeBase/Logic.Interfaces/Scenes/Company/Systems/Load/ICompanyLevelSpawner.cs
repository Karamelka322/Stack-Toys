using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load
{
    public interface ICompanyLevelSpawner
    {
        BoolReactiveProperty IsLoaded { get; }
    }
}