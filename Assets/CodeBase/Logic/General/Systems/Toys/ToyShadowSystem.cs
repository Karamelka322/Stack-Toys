using System;
using System.Collections.Generic;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Factories.Toys;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.General.Systems.Toys
{
    public class ToyShadowSystem : IDisposable, IToyShadowSystem
    {
        private static readonly Vector3 _offset = new Vector3(0, -0.05f, 1);
        
        private readonly IToyShadowFactory _toyShadowFactory;
        private readonly CompositeDisposable _compositeDisposable;
        
        private readonly Dictionary<ToyMediator, GameObject> _shadows;

        public ToyShadowSystem(IToyShadowFactory toyShadowFactory)
        {
            _toyShadowFactory = toyShadowFactory;
            
            _shadows = new Dictionary<ToyMediator, GameObject>();
            _compositeDisposable = new CompositeDisposable();

            Observable.EveryUpdate().Subscribe(OnUpdate).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public async UniTask AddAsync(ToyMediator toyMediator)
        {
            var shadow = await _toyShadowFactory.SpawnAsync();
            _shadows.Add(toyMediator, shadow);
        }

        public void Remove(ToyMediator toyMediator)
        {
            if (_shadows.TryGetValue(toyMediator, out var shadow))
            {
                Object.Destroy(shadow);
            }
            
            _shadows.Remove(toyMediator);
        }
        
        private void OnUpdate(long tick)
        {
            foreach (var pair in _shadows)
            {
                if (pair.Key.gameObject == null)
                {
                    pair.Value.transform.position = pair.Key.transform.position + _offset;
                }
                else
                {
                    Object.Destroy(pair.Value);
                    _shadows.Remove(pair.Key);
                    
                    return;
                }
            }
        }
    }
}