using CodeBase.Data.Constants;
using CodeBase.Data.Models.Audio;
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

        public async UniTask<AudioClipSettingData> GetAudioClipDataAsync(string addressableKey)
        {
            await _prepareTask;
            
            foreach (var compositionData in _config.Compositions)
            {
                if (compositionData.AddressableName == addressableKey)
                {
                    return compositionData;
                }
            }
            
            return new AudioClipSettingData();
        }

        private async UniTask PrepareResourcesAsync()
        {
            _config = await _assetServices.LoadAsync<AudioSettings>(AddressableNames.AudioSettings);
        }
    }
}