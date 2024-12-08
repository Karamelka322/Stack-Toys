using System;
using System.Collections.Generic;
using System.Threading;
using CodeBase.Data.Constants;
using CodeBase.Logic.General.Unity.Audio;
using CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Audio;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.General.Services.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.General.Services.Audio
{
    public class AudioService : IAudioService, IDisposable
    {
        private readonly IAudioSettingsProvider _audioSettingsProvider;
        private readonly IAssetServices _assetServices;
        private readonly AsyncLazy _prepareTask;

        private readonly Dictionary<string, CancellationTokenSource> _audioClips;

        private AudioListenerMediator _listener;

        public AudioService(IAssetServices assetServices, IAudioSettingsProvider audioSettingsProvider)
        {
            _audioSettingsProvider = audioSettingsProvider;
            _assetServices = assetServices;

            _audioClips = new Dictionary<string, CancellationTokenSource>();
            
            _prepareTask = UniTask.Lazy(PrepareAsync);
        }
        
        public void Dispose()
        {
            foreach (var audioData in _audioClips)
            {
                if (audioData.Value.IsCancellationRequested == false)
                {
                    audioData.Value.Cancel();
                    audioData.Value.Dispose();
                }
            }
            
            _audioClips.Clear();
        }
        
        public async UniTask PlayAsync(string addressableName, AudioOutputType audioOutputType)
        {
            await PlayAsync(addressableName, audioOutputType, false);
        }
        
        public async UniTask PlaySequenceAsync(string id, string[] addressableNames, AudioOutputType audioOutputType, bool isLoop)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            
            if (isLoop)
            {
                while (cancellationTokenSource.IsCancellationRequested == false)
                {
                    foreach (var addressableName in addressableNames)
                    {
                        await PlayAsync(addressableName, audioOutputType, false, cancellationTokenSource);

                        if (cancellationTokenSource.IsCancellationRequested)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (var addressableName in addressableNames)
                {
                    await PlayAsync(addressableName, audioOutputType, false, cancellationTokenSource);
                }
            }
        }

        public async UniTask PlayAsync(string addressableName, AudioOutputType audioOutputType, bool isLoop)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            await PlayAsync(addressableName, audioOutputType, isLoop, cancellationTokenSource);
        }

        public async UniTask PlayAsync(string addressableName, AudioOutputType audioOutputType, bool isLoop, CancellationTokenSource cancellationTokenSource)
        {
            await _prepareTask;

            var source = await SpawnSourceAsync(audioOutputType);
            var audioClip = await _assetServices.LoadAsync<AudioClip>(addressableName);
            var data = await _audioSettingsProvider.GetCompositionDataAsync(addressableName);
            
            source.clip = audioClip;
            source.volume = data.Volume;
            source.pitch = data.Pitch;
            source.loop = isLoop;
            source.Play();

            _audioClips.Add(addressableName, cancellationTokenSource);
            
            try
            {
                await UniTask.WaitWhile(() => source.isPlaying, cancellationToken: cancellationTokenSource.Token);

                _audioClips.Remove(addressableName);
                Object.Destroy(source.gameObject);
            }
            catch (OperationCanceledException e) { }
        }

        private async UniTask PrepareAsync()
        {
            _listener = await SpawnListenerAsync();
        }
        
        private async UniTask<AudioSource> SpawnSourceAsync(AudioOutputType audioOutputType)
        {
            var addressableName = audioOutputType == AudioOutputType.Music 
                ? AddressableNames.MusicSource 
                : AddressableNames.SoundSource;
            
            var parent = audioOutputType == AudioOutputType.Music
                ? _listener.MusicSourceParent
                : _listener.SoundsSourceParent;
            
            var prefab = await _assetServices.LoadAsync<GameObject>(addressableName);
            var source = Object.Instantiate(prefab, parent).GetComponent<AudioSource>();
            
            return source;
        }
        
        private async UniTask<AudioListenerMediator> SpawnListenerAsync()
        {
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableNames.AudioListener);
            var listener = Object.Instantiate(prefab).GetComponent<AudioListenerMediator>();
            
            Object.DontDestroyOnLoad(listener.gameObject);
            
            return listener;
        }
    }
    
    public enum AudioOutputType
    {
        Music,
        Sounds
    }
}