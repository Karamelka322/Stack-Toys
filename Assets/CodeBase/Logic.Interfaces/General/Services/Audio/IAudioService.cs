using CodeBase.Data.General.Enums;
using CodeBase.Logic.General.Services.Audio;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.General.Services.Audio
{
    public interface IAudioService
    {
        UniTask PlayAsync(string eventName, AudioOutputType audioOutputType);
        UniTask PlayAsync(string eventName, AudioOutputType audioOutputType, bool isLoop);
        UniTask PlaySequenceAsync(string sequenceid, string[] eventNames, AudioOutputType audioOutputType, bool isLoop);
        UniTask PlayRandomAsync(string groupId, AudioOutputType audioOutputType);
        void Stop(string id);
        void SetMusicVolume(float volume);
        void SetSoundsVolume(float volume);
    }
}