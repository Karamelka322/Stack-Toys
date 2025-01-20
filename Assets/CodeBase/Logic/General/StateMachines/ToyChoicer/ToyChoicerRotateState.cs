using System;
using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Systems.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.ToyChoicer
{
    public class ToyChoicerRotateState : BaseState
    {
        private const float RotateAngle = 15f;
        private const float RotateSpeed = 1f;
        private const float RotateDuration = 4f;
        private const float RotateSmooth = 2f;

        private readonly ToyChoicerMediator _choicer;
        private readonly ToyMediator _toy1;
        private readonly ToyMediator _toy2;

        private readonly CompositeDisposable _compositeDisposable;
        private readonly IToyRotateAnimation _toyRotateAnimation;

        private Vector3 _nextRotate;

        public ToyChoicerRotateState(
            ToyChoicerMediator choicer,
            ToyMediator toy1,
            ToyMediator toy2,
            IToyRotateAnimation toyRotateAnimation)
        {
            _choicer = choicer;
            _toy1 = toy1;
            _toy2 = toy2;
            _toyRotateAnimation = toyRotateAnimation;

            _nextRotate = new Vector3(0, 0, RotateAngle);
            _compositeDisposable = new CompositeDisposable();
        }

        public class Factory : PlaceholderFactory<ToyChoicerMediator, ToyMediator, ToyMediator, ToyChoicerRotateState> { }
        
        public override void Enter()
        {
            _toyRotateAnimation.Play(_toy1);
            _toyRotateAnimation.Play(_toy2);

            Observable.EveryUpdate()
                .Subscribe(OnUpdate).AddTo(_compositeDisposable);
            
            Observable.Interval(TimeSpan.FromSeconds(RotateDuration))
                .Subscribe(OnInterval).AddTo(_compositeDisposable);
        }
        
        public override void Exit()
        {
            _compositeDisposable?.Dispose();
            
            _toyRotateAnimation.Stop(_toy1);
            _toyRotateAnimation.Stop(_toy2);
        }

        private void OnUpdate(long _)
        {
            _choicer.transform.rotation = Quaternion.Lerp(_choicer.transform.rotation, 
                Quaternion.Euler(_nextRotate), Time.deltaTime * RotateSpeed);
        }
        
        private void OnInterval(long _)
        {
            var nextRotate = new Vector3(0, 0, _nextRotate.z < 0 ? RotateAngle : -RotateAngle);
            
            DOVirtual.Vector3(_nextRotate, nextRotate,
                RotateSmooth, (value => _nextRotate = value));
        }
    }

    public class ToyChoiceTransition : BaseTransition
    {
        private readonly ToyChoicerMediator _choicer;
        private readonly ToyMediator _toy1;
        private readonly ToyMediator _toy2;

        public ToyChoiceTransition(ToyChoicerMediator choicer, ToyMediator toy1, ToyMediator toy2)
        {
            _toy2 = toy2;
            _toy1 = toy1;
            _choicer = choicer;
        }

        public class Factory : PlaceholderFactory<ToyChoicerMediator, ToyMediator, ToyMediator, ToyChoiceTransition> { }

        public override void Enter()
        {
            base.Enter();
        }
        
        public override void Exit()
        {
            base.Exit();
        }
    }
}