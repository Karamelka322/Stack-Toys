using System;
using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish;
using CodeBase.Logic.Scenes.Company.Providers.Objects.FinishLine;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Presenters.Finish
{
    public class FinishEffectPresenter : IDisposable
    {
        private readonly IAssetServices _assetServices;
        private readonly IDisposable _disposable;
        private readonly IFinishLineProvider _finishLineProvider;

        public FinishEffectPresenter(IAssetServices assetServices, IFinishObserver finishObserver, IFinishLineProvider finishLineProvider)
        {
            _finishLineProvider = finishLineProvider;
            _assetServices = assetServices;

            _disposable = finishObserver.IsFinished.Subscribe(OnFinishValueChanged);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnFinishValueChanged(bool isFinished)
        {
            if (isFinished == false)
            {
                return;
            }
            
            SpawnEffectAsync().Forget();
        }
        
        private async UniTask SpawnEffectAsync()
        {
            var position = _finishLineProvider.FinishLine.Value.transform.position;
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableNames.CompanyScene.FinishEffect);
            var effect = Object.Instantiate(prefab, position, Quaternion.identity);
        }
    }
}