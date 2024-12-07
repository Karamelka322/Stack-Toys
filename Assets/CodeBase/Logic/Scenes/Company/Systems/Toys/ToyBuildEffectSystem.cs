using System;
using System.Threading;
using CodeBase.Data.Constants;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public class ToyBuildEffectSystem : IDisposable
    {
        private const float Delay = 0.08f;
        private const float Duration = 0.4f;
        
        private readonly IToyTowerObserver _toyTowerObserver;
        private readonly IDisposable _disposable;
        private readonly IAssetServices _assetServices;
        private readonly IToyDestroyer _toyDestroyer;
        
        private CancellationTokenSource _animationTokenSource;

        public ToyBuildEffectSystem(IToyTowerObserver toyTowerObserver, IToyDestroyer toyDestroyer, IAssetServices assetServices)
        {
            _toyDestroyer = toyDestroyer;
            _assetServices = assetServices;
            _toyTowerObserver = toyTowerObserver;

            _toyDestroyer.OnDestroyAll += OnToyDestroy;
            _disposable = toyTowerObserver.Tower.ObserveAdd().Subscribe(OnAddToy);
        }

        public void Dispose()
        {
            _toyDestroyer.OnDestroyAll -= OnToyDestroy;
            _disposable?.Dispose();

            StopEffect();
        }

        private void OnToyDestroy()
        {
            StopEffect();
        }

        private void OnAddToy(CollectionAddEvent<ToyMediator> addEvent)
        {
            StopEffect();
            
            _animationTokenSource = new CancellationTokenSource();
            
            ShowEffectAsync(_animationTokenSource.Token).Forget();
        }

        private async UniTask ShowEffectAsync(CancellationToken token)
        {
            var highlightedMaterial = await _assetServices.LoadAsync<Material>(AddressableNames.HighlightedToyMaterial);
            
            try
            {
                foreach (var toy in _toyTowerObserver.Tower)
                {
                    PlayToyAnimationAsync(toy, highlightedMaterial).Forget();

                    await UniTask.Delay(TimeSpan.FromSeconds(Delay), cancellationToken: token);
                }
            }
            catch (OperationCanceledException e) { }
        }

        private static async UniTask PlayToyAnimationAsync(ToyMediator toy, Material highlightedMaterial)
        {
            var sharedMaterial = toy.MeshRenderer.sharedMaterial;
            var sequence = DOTween.Sequence(toy);
            
            sequence.Append(DOVirtual.Float(0f, 1f, Duration, value =>
            {
                toy.MeshRenderer.material.Lerp(toy.MeshRenderer.material, highlightedMaterial, value);
            }));
            
            sequence.Append(DOVirtual.Float(0f, 1f, Duration, value =>
            {
                toy.MeshRenderer.material.Lerp(toy.MeshRenderer.material, sharedMaterial, value);
            }));

            await sequence.AsyncWaitForCompletion();
            
            if (toy != null)
            {
                toy.MeshRenderer.material = sharedMaterial;
            }
        }

        private void StopEffect()
        {
            if (_animationTokenSource?.IsCancellationRequested == false)
            {
                _animationTokenSource?.Cancel();
                _animationTokenSource?.Dispose();
            }
        }
    }
}