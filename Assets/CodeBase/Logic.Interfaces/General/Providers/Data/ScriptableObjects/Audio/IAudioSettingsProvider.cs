using CodeBase.Data.Models.Audio;
using CodeBase.Data.ScriptableObjects.Audio;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Audio
{
    public interface IAudioSettingsProvider
    {
        UniTask<AudioClipEventData> GetEventDataAsync(string eventName);
        UniTask<AudioGroupEventData> GetEventGroupDataAsync(string eventName);
    }
}