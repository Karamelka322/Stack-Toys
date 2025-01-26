using CodeBase.Logic.General.Systems.ToyChoicer;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects;
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

        public void Register(ToyChoicer choicer)
        {
            ToyChoicers.Add(choicer);
        }
        
        public void Unregister(ToyChoicer choicer)
        {
            ToyChoicers.Remove(choicer);
        }
    }
}