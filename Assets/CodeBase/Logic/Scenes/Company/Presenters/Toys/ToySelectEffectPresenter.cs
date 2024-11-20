using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Logic.General.Providers.Objects.Toys;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Presenters.Toys
{
    public class ToySelectEffectPresenter : IDisposable
    {
        private readonly IToySelectEffectFactory _toySelectEffectFactory;
        private readonly IDisposable _disposable;

        private Dictionary<ToyMediator, GameObject> _effects = new();

        public ToySelectEffectPresenter(IToyProvider toyProvider, IToySelectEffectFactory toySelectEffectFactory)
        {
            _toySelectEffectFactory = toySelectEffectFactory;
            _disposable = toyProvider.Toys.ObserveAdd().Subscribe(OnToyAdd);
        }

        private void OnToyAdd(CollectionAddEvent<(ToyMediator, ToyStateMachine)> addEvent)
        {
            addEvent.Value.Item2.SubscribeToExitState<ToyBabbleState>(() => OnToyReady(addEvent.Value.Item1));
        }
        
        private void OnToyReady(ToyMediator toyMediator)
        {
            _toySelectEffectFactory.SpawnAsync(toyMediator.transform).Forget();
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}