using CodeBase.Data.General.Constants;
using CodeBase.Data.General.Enums;
using CodeBase.Logic.General.Unity.Audio;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Factories.Audio
{
    public class AudioFactory : IAudioFactory
    {
        private readonly IAssetService _assetService;

        public AudioFactory(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask<AudioSource> SpawnSourceAsync(AudioOutputType audioOutputType, Transform parent)
        {
            var addressableName = audioOutputType == AudioOutputType.Music 
                ? AddressableConstants.MusicSource 
                : AddressableConstants.SoundSource;
            
            var prefab = await _assetService.LoadAsync<GameObject>(addressableName);
            var source = Object.Instantiate(prefab, parent).GetComponent<AudioSource>();
            
            return source;
        }
        
        public async UniTask<AudioListenerMediator> SpawnListenerAsync()
        {
            var prefab = await _assetService.LoadAsync<GameObject>(AddressableConstants.AudioListener);
            var listener = Object.Instantiate(prefab).GetComponent<AudioListenerMediator>();
            
            Object.DontDestroyOnLoad(listener.gameObject);
            
            return listener;
        }
    }
}