using System;
using CodeBase.Data.Constants;
using CodeBase.Logic.General.Services.Audio;
using CodeBase.Logic.Interfaces.General.Services.Audio;
using CodeBase.Logic.Scenes.Company.Systems.Ready;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Music
{
    public class CompanySceneMusic : IDisposable
    {
        private readonly IAudioService _audioService;
        private readonly IDisposable _disposable;

        public CompanySceneMusic(IAudioService audioService, ICompanySceneReady companySceneReady)
        {
            _audioService = audioService;

            _disposable = companySceneReady.IsReady.Subscribe(OnSceneReady);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        private void OnSceneReady(bool isReady)
        {
            if (isReady == false)
            {
                return;
            }
            
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