using CodeBase.Data.General.Models.Audio;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Audio
{
    public interface IAudioSettingsProvider
    {
        UniTask<AudioClipEventData> GetEventDataAsync(string eventName);
        UniTask<AudioGroupEventData> GetEventGroupDataAsync(string eventName);
    }
}