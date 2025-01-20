using System;
using System.Collections.Generic;
using CodeBase.Data.General.Runtime;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Systems.Toys;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Systems.Toys
{
    public class ToyRotateAnimation : IToyRotateAnimation, IDisposable
    {
        private const float ScaleChange = 0.3f;
        private const float RotationSpeed = 50f;
        private const float ScaleSpeed = 0.5f;
        private const float ResetScaleDuration = 0.4f;

        private readonly Dictionary<ToyMediator, ToyRotateAnimationData> _animations;
        
        public ToyRotateAnimation()
        {
            _animations = new Dictionary<ToyMediator, ToyRotateAnimationData>();
        }
        
        public void Dispose()
        {
            foreach (var data in _animations)
            {
                data.Value.CompositeDisposable?.Dispose();
            }
        }

        public void Play(ToyMediator toyMediator)
        {
            var data = new ToyRotateAnimationData()
            {
                CompositeDisposable = new CompositeDisposable(),
                StartScale = toyMediator.transform.localScale,
                Scale = Vector3.zero,
                RotationAxis = Vector3.zero,
            };
            
            UpdateAnimationValues(data);
            
            _animations.Add(toyMediator, data);
            
            Observable.EveryUpdate().Subscribe(
                _ => OnUpdate(toyMediator, data)).AddTo(data.CompositeDisposable);
            
            Observable.Interval(TimeSpan.FromSeconds(0.8f)).Subscribe(
                _ => OnInterval(toyMediator, data)).AddTo(data.CompositeDisposable);
        }
    
        public void Stop(ToyMediator toyMediator)
        {
            if (_animations.TryGetValue(toyMediator, out var data) == false)
            {
                return;
            }

            data.CompositeDisposable?.Dispose();

            toyMediator.transform.DOScale(data.StartScale, ResetScaleDuration);

            _animations.Remove(toyMediator);
        }
        
        private static void OnUpdate(ToyMediator toyMediator, ToyRotateAnimationData data)
        {
            var transform = toyMediator.transform;
            
            transform.Rotate(data.RotationAxis, RotationSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, data.Scale, Time.deltaTime * ScaleSpeed);
        }

        private static void OnInterval(ToyMediator toyMediator, ToyRotateAnimationData data)
        {
            UpdateAnimationValues(data);
        }
        
        private static void UpdateAnimationValues(ToyRotateAnimationData data)
        {
            data.RotationAxis = Vector3.Lerp(data.RotationAxis, GetRandomDirection(), Time.deltaTime);
            data.Scale = GetRandomScale(data.StartScale);
        }
        
        private static Vector3 GetRandomDirection()
        {
            return new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f));
        }
        
        private static Vector3 GetRandomScale(Vector3 startScale)
        {
            return new Vector3(
                UnityEngine.Random.Range(startScale.x - ScaleChange, startScale.x + ScaleChange),
                UnityEngine.Random.Range(startScale.y - ScaleChange, startScale.y + ScaleChange),
                UnityEngine.Random.Range(startScale.z - ScaleChange, startScale.z + ScaleChange));
        }
    }
}