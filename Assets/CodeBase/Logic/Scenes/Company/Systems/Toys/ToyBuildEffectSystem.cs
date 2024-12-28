using System;
using System.Collections.Generic;
using CodeBase.Data.Constants;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Toys;
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
        
        public ToyBuildEffectSystem(IToyTowerObserver toyTowerObserver, IAssetServices assetServices)
        {
            _assetServices = assetServices;
            _toyTowerObserver = toyTowerObserver;

            _disposable = toyTowerObserver.Tower.ObserveAdd().Subscribe(OnAddToy);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnAddToy(CollectionAddEvent<ToyMediator> addEvent)
        {
            ShowEffectAsync().Forget();
        }

        private async UniTask ShowEffectAsync()
        {
            var highlightedMaterial = await _assetServices.LoadAsync<Material>(AddressableConstants.HighlightedToyMaterial);
            
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

            if (toy != null)
            {
                toy.MeshRenderer.SetMaterials(sharedMaterials);
            }
        }
    }
}