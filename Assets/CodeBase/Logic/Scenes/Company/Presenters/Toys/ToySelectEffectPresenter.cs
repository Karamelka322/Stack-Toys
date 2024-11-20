using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Presenters.Toys
{
    public class ToySelectEffectPresenter : IDisposable
    {
        private readonly IToySelectEffectFactory _toySelectEffectFactory;
        private readonly IDisposable _disposable;

        private GameObject _effect;

        public ToySelectEffectPresenter(IToySelectObserver toySelectObserver, IToySelectEffectFactory toySelectEffectFactory)
        {
            _toySelectEffectFactory = toySelectEffectFactory;
            
            _disposable = toySelectObserver.Toy.Subscribe(OnToySelect);
        }

        private async void OnToySelect(ToyMediator toy)
        {
            if (toy == null)
            {
                return;
            }
            
            _effect = await _toySelectEffectFactory.SpawnAsync(toy.transform);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}