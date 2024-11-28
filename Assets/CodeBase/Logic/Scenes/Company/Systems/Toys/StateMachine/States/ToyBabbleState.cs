using System;
using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Factories.Babble;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States
{
    public class ToyBabbleState : BaseState
    {
        private const float ScaleChange = 0.2f;
        private const float RotationSpeed = 60f;
        private const float ScaleSpeed = 1f;

        private readonly ToyMediator _toyMediator;
        private readonly IBabbleFactory _babbleFactory;
        private readonly Vector3 _startScale;

        private Vector3 _scale;
        private Vector3 _rotationAxis;

        private GameObject _babble;
        private CompositeDisposable _compositeDisposable;

        public ToyBabbleState(ToyMediator toyMediator, IBabbleFactory babbleFactory)
        {
            _babbleFactory = babbleFactory;
            _toyMediator = toyMediator;
            _startScale = toyMediator.transform.localScale;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyBabbleState> { }
        
        public override async void Enter()
        {
            _compositeDisposable = new CompositeDisposable();
            
            _babble = await _babbleFactory.SpawnAsync(_toyMediator.transform.position, _toyMediator.transform);
            
            UpdateAnimationValues();
            
            Observable.EveryUpdate().Subscribe(OnUpdate).AddTo(_compositeDisposable);
            Observable.Interval(TimeSpan.FromSeconds(0.8f)).Subscribe(OnInterval).AddTo(_compositeDisposable);
        }

        public override void Exit()
        {
            _compositeDisposable?.Dispose();
            Object.Destroy(_babble);
        }

        private void OnUpdate(long _)
        {
            var transform = _toyMediator.transform;
            
            transform.Rotate(_rotationAxis, RotationSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, _scale, Time.deltaTime * ScaleSpeed);
        }

        private void OnInterval(long _)
        {
            UpdateAnimationValues();
        }

        private void UpdateAnimationValues()
        {
            _rotationAxis = Vector3.Lerp(_rotationAxis, GetRandomDirection(), Time.deltaTime);
            _scale = GetRandomScale();
        }
     
        private Vector3 GetRandomDirection()
        {
            return new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f));
        }
        
        private Vector3 GetRandomScale()
        {
            return new Vector3(
                UnityEngine.Random.Range(_startScale.x - ScaleChange, _startScale.x + ScaleChange),
                UnityEngine.Random.Range(_startScale.y - ScaleChange, _startScale.y + ScaleChange),
                UnityEngine.Random.Range(_startScale.z - ScaleChange, _startScale.z + ScaleChange));
        
        }
    }
}