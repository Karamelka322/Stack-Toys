using CodeBase.Logic.General.Services.Audio;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.General.Services.Audio
{
    public interface IAudioService
    {
        UniTask PlayAsync(string addressableName, AudioOutputType audioOutputType);
        UniTask PlayAsync(string addressableName, AudioOutputType audioOutputType, bool isLoop);
        UniTask PlaySequenceAsync(string id, string[] addressableNames, AudioOutputType audioOutputType, bool isLoop);
    }
}