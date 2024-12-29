using System;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.FinishLine;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Presenters.Finish
{
    public class FinishEffectPresenter : IDisposable
    {
        private readonly IAssetService _assetService;
        private readonly IDisposable _disposable;
        private readonly IFinishLineProvider _finishLineProvider;
        
        public FinishEffectPresenter(IAssetService assetService, IFinishObserver finishObserver,
            IFinishLineProvider finishLineProvider)
        {
            _finishLineProvider = finishLineProvider;
            _assetService = assetService;

            _disposable = finishObserver.IsFinished.Subscribe(OnFinishValueChanged);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private async void OnFinishValueChanged(bool isFinished)
        {
            if (isFinished == false)
            {
                return;
            }

            var effect = await SpawnEffectAsync();
            Object.Destroy(effect, 10f);
        }
        
        private async UniTask<GameObject> SpawnEffectAsync()
        {
            var finishLine = _finishLineProvider.FinishLine.Value.transform;
            var addressableName = AddressableConstants.CompanyScene.FinishEffect;
            var prefab = await _assetService.LoadAsync<GameObject>(addressableName);
            
            return Object.Instantiate(prefab, finishLine.position, finishLine.rotation);
        }
    }
}