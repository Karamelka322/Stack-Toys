using System;
using CodeBase.Data.Constants;
using CodeBase.Data.Enums;
using CodeBase.Logic.General.Services.Audio;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Audio;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Ready;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Presenters.Toys
{
    public class ToySelectSoundPresenter : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly IAudioService _audioService;
        private readonly ICompanySceneReady _companySceneReady;

        public ToySelectSoundPresenter(
            IToySelectObserver toySelectObserver,
            ICompanySceneReady companySceneReady,
            IAudioService audioService)
        {
            _companySceneReady = companySceneReady;
            _audioService = audioService;
            
            _disposable = toySelectObserver.Toy.Subscribe(OnSelectedToyChanged);
        }

        private void OnSelectedToyChanged(ToyMediator toyMediator)
        {
            if (_companySceneReady.IsReady.Value == false)
            {
                return;
            }
            
            _audioService.PlayAsync(AddressableNames.ToySelectSound, AudioOutputType.Sounds).Forget();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}