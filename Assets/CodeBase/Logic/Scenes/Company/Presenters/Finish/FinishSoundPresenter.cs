using System;
using CodeBase.Data.Constants;
using CodeBase.Data.Enums;
using CodeBase.Logic.Interfaces.General.Services.Audio;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Presenters.Finish
{
    public class FinishSoundPresenter : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly IAudioService _audioService;

        public FinishSoundPresenter(IFinishObserver finishObserver, IAudioService audioService)
        {
            _audioService = audioService;
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

            _audioService.PlayAsync(AudioConstants.FinishSound, AudioOutputType.Sounds);
        }
    }
}