using CodeBase.Logic.Interfaces.Scenes.Infinity.Observers.Toys;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Objects;

namespace CodeBase.Logic.Scenes.Infinity.Observers.Toys
{
    public class ToyChoiceObserver : IToyChoiceObserver
    {
        private readonly IToyChoicerProvider _toyChoicerProvider;

        public ToyChoiceObserver(IToyChoicerProvider toyChoicerProvider)
        {
            _toyChoicerProvider = toyChoicerProvider;
        }
    }
}