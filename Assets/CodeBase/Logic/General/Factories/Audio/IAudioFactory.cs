using CodeBase.Data.Enums;
using CodeBase.Logic.General.Unity.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Factories.Audio
{
    public interface IAudioFactory
    {
        UniTask<AudioSource> SpawnSourceAsync(AudioOutputType audioOutputType, Transform parent);
        UniTask<AudioListenerMediator> SpawnListenerAsync();
    }
}