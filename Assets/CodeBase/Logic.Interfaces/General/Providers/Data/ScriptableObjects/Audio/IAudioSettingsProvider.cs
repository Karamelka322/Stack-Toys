using CodeBase.Data.Models.Audio;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Audio
{
    public interface IAudioSettingsProvider
    {
        UniTask<AudioVolumeData> GetCompositionDataAsync(string addressableKey);
    }
}