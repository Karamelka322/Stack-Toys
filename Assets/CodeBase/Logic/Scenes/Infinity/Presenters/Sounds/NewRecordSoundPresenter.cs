using System;
using CodeBase.Data.General.Constants;
using CodeBase.Data.General.Enums;
using CodeBase.Logic.Interfaces.General.Services.Audio;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Records;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Presenters.Sounds
{
    public class NewRecordSoundPresenter : IDisposable
    {
        private readonly IAudioService _audioService;
        private readonly IInfinityRecordSystem _recordSystem;
        private readonly IDisposable _disposable;

        public NewRecordSoundPresenter(IAudioService audioService, IInfinityRecordSystem recordSystem)
        {
            _recordSystem = recordSystem;
            _audioService = audioService;
            
            _disposable = recordSystem.PlayerRecord.Subscribe(OnPlayerRecordChanged);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnPlayerRecordChanged(float record)
        {
            if (_recordSystem.IsReady.Value == false)
            {
                return;
            }
            
            _audioService.PlayAsync(AudioConstants.FinishSound, AudioOutputType.Sounds);
        }
    }
}