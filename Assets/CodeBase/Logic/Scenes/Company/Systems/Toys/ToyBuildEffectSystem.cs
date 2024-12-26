using System;
using System.Collections.Generic;
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
            
            foreach (var toy in _toyTowerObserver.Tower)
            {
                PlayToyAnimationAsync(toy, highlightedMaterial).Forget();
            }
        }
        
        private static async UniTask PlayToyAnimationAsync(ToyMediator toy, Material highlightedMaterial)
        {
            var sharedMaterials = new List<Material>();
            var materials = new List<Material>();
            
            toy.MeshRenderer.GetSharedMaterials(sharedMaterials);
            toy.MeshRenderer.GetMaterials(materials);

            var sequence = DOTween.Sequence(toy);
            
            sequence.Append(DOVirtual.Float(0f, 1f, Duration,
                value =>
                {
                    foreach (var material in materials)
                    {
                        material.Lerp(material, highlightedMaterial, value);
                    }
                }));

            sequence.Append(DOVirtual.Float(0f, 1f, Duration,
                value =>
                {
                    for (var i = 0; i < materials.Count; i++)
                    {
                        materials[i].Lerp(materials[i], sharedMaterials[i], value);
                    }
                }));

            await sequence.AsyncWaitForCompletion();

            toy.MeshRenderer.SetMaterials(sharedMaterials);
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