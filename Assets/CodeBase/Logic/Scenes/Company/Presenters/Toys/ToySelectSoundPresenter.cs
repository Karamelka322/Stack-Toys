using System;
using CodeBase.Data.General.Constants;
using CodeBase.Data.General.Enums;
using CodeBase.Logic.General.Services.Audio;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Audio;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Toys;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Presenters.Toys
{
    public class ToySelectSoundPresenter : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly IAudioService _audioService;
        private readonly ICompanySceneReadyObserver _companySceneReady;

        public ToySelectSoundPresenter(
            IToySelectObserver toySelectObserver,
            ICompanySceneReadyObserver companySceneReady,
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
            
            _audioService.PlayAsync(AudioConstants.ToySelectSound, AudioOutputType.Sounds).Forget();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}