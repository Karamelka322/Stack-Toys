using System;
using CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes;
using CodeBase.Logic.General.Unity.Toys;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States
{
    public class ToyBabbleState : BaseState
    {
        private const float AnimationInterval = 2f;
        private const float ScaleChange = 0.05f;
        
        private readonly ToyMediator _toyMediator;
        private readonly IBabbleFactory _babbleFactory;
        
        private GameObject _babble;
        private IDisposable _disposable;

        public ToyBabbleState(ToyMediator toyMediator, IBabbleFactory babbleFactory)
        {
            _babbleFactory = babbleFactory;
            _toyMediator = toyMediator;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyBabbleState> { }
        
        public override async void Enter()
        {
            _babble = await _babbleFactory.SpawnAsync(_toyMediator.transform.position, _toyMediator.transform);
            _disposable = Observable.Interval(TimeSpan.FromSeconds(AnimationInterval)).Subscribe(StartAnimation);
        }

        public override void Exit()
        {
            _disposable?.Dispose();
            Object.Destroy(_babble);
        }

        private void StartAnimation(long _)
        {
            var randomRotation = new Vector3(
                UnityEngine.Random.Range(-180, 180),
                UnityEngine.Random.Range(-180, 180),
                UnityEngine.Random.Range(-180, 180));
            
            var randomScale = new Vector3(
                UnityEngine.Random.Range(_toyMediator.transform.localScale.x - ScaleChange, _toyMediator.transform.localScale.x + ScaleChange),
                UnityEngine.Random.Range(_toyMediator.transform.localScale.y - ScaleChange, _toyMediator.transform.localScale.y + ScaleChange),
                UnityEngine.Random.Range(_toyMediator.transform.localScale.z - ScaleChange, _toyMediator.transform.localScale.z + ScaleChange));
            
            _toyMediator.transform.DORotate(randomRotation, AnimationInterval);
            _toyMediator.transform.DOScale(randomScale, AnimationInterval);
        }
    }
}