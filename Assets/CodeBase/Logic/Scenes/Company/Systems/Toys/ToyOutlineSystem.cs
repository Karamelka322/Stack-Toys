using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Presenters.Toys
{
    public class ToyOutlineSystem : IDisposable
    {
        private readonly IToySelectEffectFactory _toySelectEffectFactory;
        private readonly IToySelectObserver _toySelectObserver;
        private readonly IDisposable _selectDisposable;
        
        private IDisposable _updateDisposable;
        private GameObject _outline;

        public ToyOutlineSystem(IToySelectObserver toySelectObserver, IToySelectEffectFactory toySelectEffectFactory)
        {
            _toySelectObserver = toySelectObserver;
            _toySelectEffectFactory = toySelectEffectFactory;
            
            _selectDisposable = toySelectObserver.Toy.Subscribe(OnSelectedToyChanged);
        }

        public void Dispose()
        {
            _selectDisposable?.Dispose();
        }
        
        private async void OnSelectedToyChanged(ToyMediator toyMediator)
        {
            if (toyMediator == null)
            {
                if (_outline != null)
                {
                    _updateDisposable?.Dispose();
                    Object.Destroy(_outline);
                }
                
                return;
            }

            _outline = await _toySelectEffectFactory.SpawnAsync(null);
            _updateDisposable = Observable.EveryUpdate().Subscribe(OnUpdate);
        }
        
        private void OnUpdate(long tick)
        {
            if (_outline == null || _toySelectObserver.Toy.Value == null)
            {
                return;
            }
            
            _outline.transform.position = _toySelectObserver.Toy.Value.transform.position;
        }
    }
}