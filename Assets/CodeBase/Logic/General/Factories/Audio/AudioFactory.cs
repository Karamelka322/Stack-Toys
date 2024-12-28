using CodeBase.Data.Constants;
using CodeBase.Data.Enums;
using CodeBase.Logic.General.Unity.Audio;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Factories.Audio
{
    public class AudioFactory : IAudioFactory
    {
        private readonly IAssetServices _assetServices;

        public AudioFactory(IAssetServices assetServices)
        {
            _assetServices = assetServices;
        }

        public async UniTask<AudioSource> SpawnSourceAsync(AudioOutputType audioOutputType, Transform parent)
        {
            var addressableName = audioOutputType == AudioOutputType.Music 
                ? AddressableConstants.MusicSource 
                : AddressableConstants.SoundSource;
            
            var prefab = await _assetServices.LoadAsync<GameObject>(addressableName);
            var source = Object.Instantiate(prefab, parent).GetComponent<AudioSource>();
            
            return source;
        }
        
        public async UniTask<AudioListenerMediator> SpawnListenerAsync()
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableConstants.AudioListener);
            var listener = Object.Instantiate(prefab).GetComponent<AudioListenerMediator>();
            
            Object.DontDestroyOnLoad(listener.gameObject);
            
            return listener;
        }
    }
}