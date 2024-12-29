using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects;
using CodeBase.Logic.Scenes.Infinity.Systems.Toys;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Providers.Objects
{
    public class ToyChoicerProvider : IToyChoicerProvider
    {
        public ReactiveCollection<ToyChoicer> ToyChoicers { get; }

        public ToyChoicerProvider()
        {
            ToyChoicers = new ReactiveCollection<ToyChoicer>();
        }

        public void Register(ToyChoicer toyChoicer)
        {
            ToyChoicers.Add(toyChoicer);
        }

        public void Unregister(ToyChoicer toyChoicer)
        {
            ToyChoicers.Remove(toyChoicer);
        }
    }
}