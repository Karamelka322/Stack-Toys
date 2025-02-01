using System;
using System.Collections.Generic;
using CodeBase.Data.General.Runtime;
using CodeBase.Logic.Interfaces.General.Systems.Toys;
using CodeBase.Logic.Scenes.Infinity.Unity.Toys;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Systems.ToyChoicer
{
    public class ToyChoicerRotateAnimation : IToyChoicerRotateAnimation
    {
        private const float RotateAngle = 15f;
        private const float RotateSpeed = 1f;
        private const float RotateDuration = 4f;
        private const float RotateSmooth = 2f;

        private readonly Dictionary<ToyChoicerMediator, ToyChoicerRotateAnimationData> _animations;
        
        public ToyChoicerRotateAnimation()
        {
            _animations = new Dictionary<ToyChoicerMediator, ToyChoicerRotateAnimationData>();
        }

        public void Play(ToyChoicerMediator toyChoicerMediator)
        {
            var data = new ToyChoicerRotateAnimationData()
            {
                CompositeDisposable = new CompositeDisposable(),
                NextRotate = new Vector3(0, 0, RotateAngle),
            };
            
            _animations.Add(toyChoicerMediator, data);
            
            Observable.EveryUpdate()
                .Subscribe((_) =>
                {
                    if (toyChoicerMediator != null)
                    {
                        OnUpdate(toyChoicerMediator, data);
                    }
                    else
                    {
                        data.CompositeDisposable?.Dispose();
                    }
                }).AddTo(data.CompositeDisposable);
            
            Observable.Interval(TimeSpan.FromSeconds(RotateDuration))
                .Subscribe((_) =>
                {
                    if (toyChoicerMediator != null)
                    {
                        OnInterval(toyChoicerMediator, data);
                    }
                    else
                    {
                        data.CompositeDisposable?.Dispose();
                    }
                }).AddTo(data.CompositeDisposable);
        }

        public void Stop(ToyChoicerMediator toyChoicerMediator)
        {
            _animations[toyChoicerMediator].CompositeDisposable?.Dispose();
        }

        private static void OnUpdate(ToyChoicerMediator toyChoicerMediator, ToyChoicerRotateAnimationData data)
        {
            toyChoicerMediator.transform.rotation = Quaternion.Lerp(toyChoicerMediator.transform.rotation, 
                Quaternion.Euler(data.NextRotate), Time.deltaTime * RotateSpeed);
        }
        
        private static void OnInterval(ToyChoicerMediator toyChoicerMediator, ToyChoicerRotateAnimationData data)
        {
            var nextRotate = new Vector3(0, 0, data.NextRotate.z < 0 ? RotateAngle : -RotateAngle);
            
            DOVirtual.Vector3(data.NextRotate, nextRotate,
                RotateSmooth, (value => data.NextRotate = value));
        }
    }
}