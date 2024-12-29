using System;
using System.Collections.Generic;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public class ToyShadowSystem : IDisposable
    {
        private static readonly Vector3 _offset = new Vector3(0, -0.05f, 1);
        
        private readonly IToyShadowFactory _toyShadowFactory;
        private readonly CompositeDisposable _compositeDisposable;
        private readonly Dictionary<ToyMediator, GameObject> _shadows;

        public ToyShadowSystem(IToyProvider toyProvider, IToyShadowFactory toyShadowFactory)
        {
            _toyShadowFactory = toyShadowFactory;
            
            _shadows = new Dictionary<ToyMediator, GameObject>();
            _compositeDisposable = new CompositeDisposable();

            toyProvider.Toys.ObserveAdd().Subscribe(OnToyAdd).AddTo(_compositeDisposable);
            toyProvider.Toys.ObserveRemove().Subscribe(OnToyRemove).AddTo(_compositeDisposable);
            
            Observable.EveryUpdate().Subscribe(OnUpdate).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private async void OnToyAdd(CollectionAddEvent<(ToyMediator, ToyStateMachine)> addEvent)
        {
            var shadow = await _toyShadowFactory.SpawnAsync();
            _shadows.Add(addEvent.Value.Item1, shadow);
        }
        
        private void OnToyRemove(CollectionRemoveEvent<(ToyMediator, ToyStateMachine)> removeEvent)
        {
            var toy = removeEvent.Value.Item1;

            if (_shadows.TryGetValue(toy, out var shadow))
            {
                Object.Destroy(shadow);
            }
            
            _shadows.Remove(toy);
        }
        
        private void OnUpdate(long tick)
        {
            foreach (var pair in _shadows)
            {
                pair.Value.transform.position = pair.Key.transform.position + _offset;
            }
        }
    }
}