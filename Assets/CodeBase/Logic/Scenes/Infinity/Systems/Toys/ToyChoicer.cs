using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Toys;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Systems.Toys
{
    public class ToyChoicer : IDisposable
    {
        private readonly IDisposable _disposable;
        
        private readonly ToyMediator _toy1;
        private readonly ToyMediator _toy2;
        
        public event Action OnChoice;
        
        public ToyChoicer(ToyMediator toy1, ToyMediator toy2, IToySelectObserver toySelectObserver)
        {
            _toy2 = toy2;
            _toy1 = toy1;
            
            _disposable = toySelectObserver.Toy.Subscribe(OnToySelected);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnToySelected(ToyMediator toy)
        {
            if (toy == null)
            {
                return;
            }
            
            if (toy == _toy1 || toy == _toy2)
            {
                OnChoice?.Invoke();
            }
        }
    }
}