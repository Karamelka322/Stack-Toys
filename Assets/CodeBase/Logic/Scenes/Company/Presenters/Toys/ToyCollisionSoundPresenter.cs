using System;
using CodeBase.Data.Constants;
using CodeBase.Data.Enums;
using CodeBase.Logic.General.Unity.Floors;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Services.Audio;
using CodeBase.Logic.Scenes.Company.Observers.Toys;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Presenters.Toys
{
    public class ToyCollisionSoundPresenter : IDisposable
    {
        private readonly IToyCollisionObserver _toyCollisionObserver;
        private readonly IAudioService _audioService;

        public ToyCollisionSoundPresenter(IToyCollisionObserver toyCollisionObserver, IAudioService audioService)
        {
            _audioService = audioService;
            _toyCollisionObserver = toyCollisionObserver;
            
            _toyCollisionObserver.OnCollision += OnCollision;
        }

        public void Dispose()
        {
            _toyCollisionObserver.OnCollision -= OnCollision;
        }

        private void OnCollision(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out ToyMediator toyMediator))
            {
                _audioService.PlayRandomAsync(AudioConstants.ToyCollisionWithToyGroup, AudioOutputType.Sounds);
            }

            if (gameObject.TryGetComponent(out Floor floor))
            {
                _audioService.PlayRandomAsync(AudioConstants.ToyCollisionWithGrassGroup, AudioOutputType.Sounds);
            }
        }
    }
}