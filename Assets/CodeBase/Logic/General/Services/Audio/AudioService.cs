using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.Data.Enums;
using CodeBase.Data.Models.Audio;
using CodeBase.Logic.General.Extensions;
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
        private readonly IAudioFactory _audioFactory;
        private readonly AsyncLazy _prepareTask;

        private readonly List<AudioClipPlaybackData> _audioClips;

        private AudioListenerMediator _listener;

        public AudioService(
            IAssetServices assetServices,
            IAudioSettingsProvider audioSettingsProvider,
            IAudioFactory audioFactory)
        {
            _audioFactory = audioFactory;
            _audioSettingsProvider = audioSettingsProvider;
            _assetServices = assetServices;

            _audioClips = new List<AudioClipPlaybackData>();
            
            _prepareTask = UniTask.Lazy(PrepareAsync);
        }
        
        public void Dispose()
        {
            foreach (var audioData in _audioClips)
            {
                if (audioData.CancellationTokenSource.IsCancellationRequested == false)
                {
                    audioData.CancellationTokenSource.Cancel();
                    audioData.CancellationTokenSource.Dispose();
                }
            }
            
            _audioClips.Clear();
        }
        
        public async UniTask PlayAsync(string eventName, AudioOutputType audioOutputType)
        {
            await PlayAsync(eventName, audioOutputType, false);
        }

        public async UniTask PlayRandomAsync(string groupId, AudioOutputType audioOutputType)
        {
            var groupData = await _audioSettingsProvider.GetEventGroupDataAsync(groupId);
            var randomAudioClipSettings = groupData.AudioClips.Random();
            var cancellationTokenSource = new CancellationTokenSource();
            
            await PlayAsync(groupId, randomAudioClipSettings, audioOutputType, false, cancellationTokenSource);
        }
        
        public async UniTask PlaySequenceAsync(string sequenceid, string[] eventNames, AudioOutputType audioOutputType, bool isLoop)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            
            if (isLoop)
            {
                while (cancellationTokenSource.IsCancellationRequested == false)
                {
                    foreach (var eventName in eventNames)
                    {
                        await PlayAsync(eventName, audioOutputType, false, cancellationTokenSource);

                        if (cancellationTokenSource.IsCancellationRequested)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (var eventName in eventNames)
                {
                    await PlayAsync(eventName, audioOutputType, false, cancellationTokenSource);
                }
            }
        }

        public async UniTask PlayAsync(string eventName, AudioOutputType audioOutputType, bool isLoop)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            await PlayAsync(eventName, audioOutputType, isLoop, cancellationTokenSource);
        }

        public async UniTask PlayAsync(string eventName, AudioOutputType audioOutputType, bool isLoop, CancellationTokenSource cancellationTokenSource)
        {
            await _prepareTask;

            var data = await _audioSettingsProvider.GetEventDataAsync(eventName);

            await PlayAsync(eventName, data.Settings, audioOutputType, isLoop, cancellationTokenSource);
        }

        public async UniTask PlayAsync(string id, AudioClipSettingData settingData, AudioOutputType audioOutputType, bool isLoop, CancellationTokenSource cancellationTokenSource)
        {
            await _prepareTask;

            var parent = audioOutputType == AudioOutputType.Music
                ? _listener.MusicSourceParent
                : _listener.SoundsSourceParent;

            var source = await _audioFactory.SpawnSourceAsync(audioOutputType, parent);
            var audioClip = await _assetServices.LoadAsync<AudioClip>(settingData.AudioClip.AssetGUID);
            
            source.clip = audioClip;
            source.volume = settingData.Volume;
            source.pitch = settingData.Pitch;
            source.loop = isLoop;
            source.Play();

            var playbackData = new AudioClipPlaybackData()
            {
                Id = id,
                CancellationTokenSource = cancellationTokenSource,
                Source = source,
            };

            _audioClips.Add(playbackData);
            
            try
            {
                await UniTask.WaitWhile(() => source.isPlaying, cancellationToken: cancellationTokenSource.Token);

                _audioClips.Remove(playbackData);
                Object.Destroy(source.gameObject);
            }
            catch (OperationCanceledException e) { }
        }

        private async UniTask PrepareAsync()
        {
            _listener = await _audioFactory.SpawnListenerAsync();
        }
    }
}