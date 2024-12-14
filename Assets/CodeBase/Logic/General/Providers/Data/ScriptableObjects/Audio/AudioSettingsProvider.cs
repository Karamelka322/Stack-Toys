using CodeBase.Data.Constants;
using CodeBase.Data.Models.Audio;
using CodeBase.Data.ScriptableObjects.Audio;
using CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Audio;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using AudioSettings = CodeBase.Data.ScriptableObjects.Audio.AudioSettings;

namespace CodeBase.Logic.General.Providers.Data.ScriptableObjects.Audio
{
    public class AudioSettingsProvider : IAudioSettingsProvider
    {
        private readonly IAssetServices _assetServices;
        private readonly AsyncLazy _prepareTask;
        
        private AudioSettings _config;

        public AudioSettingsProvider(IAssetServices assetServices)
        {
            _assetServices = assetServices;
            _prepareTask = UniTask.Lazy(PrepareResourcesAsync);
        }

        public async UniTask<AudioClipEventData> GetEventDataAsync(string eventName)
        {
            await _prepareTask;
            
            foreach (var eventData in _config.AudioClipEvents)
            {
                if (eventData.EventName == eventName)
                {
                    return eventData;
                }
            }
            
            return new AudioClipEventData();
        }
        
        public async UniTask<AudioGroupEventData> GetEventGroupDataAsync(string eventName)
        {
            await _prepareTask;
            
            foreach (var eventData in _config.AudioClipGroupEvents)
            {
                if (eventData.GroupId == eventName)
                {
                    return eventData;
                }
            }
            
            return new AudioGroupEventData();
        }

        private async UniTask PrepareResourcesAsync()
        {
            _config = await _assetServices.LoadAsync<AudioSettings>(AddressableNames.AudioSettings);
        }
    }
}