using System;
using CodeBase.Data.Constants;
using CodeBase.Data.Enums;
using CodeBase.Logic.Interfaces.General.Services.Audio;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Scenes.Company.Systems.Music
{
    public class CompanySceneMusic
    {
        private readonly IAudioService _audioService;
        private readonly IDisposable _disposable;

        public CompanySceneMusic(IAudioService audioService)
        {
            _audioService = audioService;

            StartPlay();
        }
        
        private void StartPlay()
        {
            _audioService.PlaySequenceAsync("Company_scene_birds_ambient_sequence", new[]
            {
                AddressableNames.CompanyScene.Birds_1_ambient,
                AddressableNames.CompanyScene.Birds_2_ambient,
            }, 
                AudioOutputType.Music, true);
            
            _audioService.PlayAsync(AddressableNames.CompanyScene.Meditation_music,
                AudioOutputType.Music, true).Forget();
            
            _audioService.PlayAsync(AddressableNames.CompanyScene.ForestNoise_ambient,
                AudioOutputType.Music, true).Forget();
        }

        private void StopPlay()
        {
            
        }
    }
}