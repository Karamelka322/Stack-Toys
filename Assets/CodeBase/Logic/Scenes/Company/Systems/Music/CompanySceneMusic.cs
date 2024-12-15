using System;
using CodeBase.Data.Constants;
using CodeBase.Data.Enums;
using CodeBase.Logic.Interfaces.General.Services.Audio;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Scenes.Company.Systems.Music
{
    public class CompanySceneMusic : IDisposable
    {
        private readonly IAudioService _audioService;

        public CompanySceneMusic(IAudioService audioService)
        {
            _audioService = audioService;

            StartPlay();
        }
        
        private void StartPlay()
        {
            _audioService.PlaySequenceAsync("Company_scene_birds_ambient_sequence", new[]
            {
                AudioConstants.Birds_1_ambient,
                AudioConstants.Birds_2_ambient,
            }, 
                AudioOutputType.Music, true);
            
            _audioService.PlayAsync(AudioConstants.Meditation_music,
                AudioOutputType.Music, true).Forget();
            
            _audioService.PlayAsync(AudioConstants.ForestNoise_ambient,
                AudioOutputType.Music, true).Forget();
        }

        private void StopPlay()
        {
            
        }

        public void Dispose()
        {
            // _audioService.s
        }
    }
}