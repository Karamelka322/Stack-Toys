using CodeBase.Logic.General.Systems.ToyChoicer;
using CodeBase.Logic.Scenes.Infinity.Systems.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects
{
    public interface IToyChoicerProvider
    {
        ReactiveCollection<ToyChoicer> ToyChoicers { get; }
        
        void Register(ToyChoicer choicer);
        void Unregister(ToyChoicer choicer);
    }
}