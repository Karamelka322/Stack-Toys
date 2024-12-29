using CodeBase.Logic.Scenes.Infinity.Systems.Toys;
using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects
{
    public interface IToyChoicerProvider
    {
        ReactiveCollection<ToyChoicer> ToyChoicers { get; }
        
        void Register(ToyChoicer toyChoicer);
        void Unregister(ToyChoicer toyChoicer);
    }
}