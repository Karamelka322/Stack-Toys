using CodeBase.Logic.Interfaces.General.Observers.Toys;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Observers.Toys
{
    public class InfinityToyCountObserver : IToyCountObserver
    {
        public IntReactiveProperty LeftAvailableNumberOfToys { get; }
        public IntReactiveProperty NumberOfOpenToys { get; }
        public IntReactiveProperty MaxNumberOfToys { get; }
        public IntReactiveProperty NumberOfTowerBuildToys { get; }

        public InfinityToyCountObserver()
        {
            LeftAvailableNumberOfToys = new IntReactiveProperty();
            NumberOfOpenToys = new IntReactiveProperty();
            MaxNumberOfToys = new IntReactiveProperty();
            NumberOfTowerBuildToys = new IntReactiveProperty();
        }
    }
}