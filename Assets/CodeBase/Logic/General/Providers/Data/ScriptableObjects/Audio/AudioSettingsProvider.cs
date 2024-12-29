using CodeBase.Data.General.Constants;
using CodeBase.Data.General.Models.Audio;
using CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Audio;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using AudioSettings = CodeBase.Data.General.ScriptableObjects.Audio.AudioSettings;

namespace CodeBase.Logic.General.Providers.Data.ScriptableObjects.Audio
{
    public class AudioSettingsProvider : IAudioSettingsProvider
    {
        private readonly IAssetService _assetService;
        private readonly AsyncLazy _prepareTask;
        
        private AudioSettings _config;

        public AudioSettingsProvider(IAssetService assetService)
        {
            _assetService = assetService;
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
            _config = await _assetService.LoadAsync<AudioSettings>(AddressableConstants.AudioSettings);
        }
    }
}