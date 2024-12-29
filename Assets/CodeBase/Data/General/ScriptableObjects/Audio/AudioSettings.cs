using CodeBase.Data.General.Constants;
using CodeBase.Data.General.Models.Audio;
using UnityEngine;

namespace CodeBase.Data.General.ScriptableObjects.Audio
{
    [CreateAssetMenu(menuName = AssetMenuConstants.ScriptableObjects + nameof(AudioSettings), fileName = nameof(AudioSettings))]
    public class AudioSettings : ScriptableObject
    {
        [SerializeField]
        private AudioClipEventData[] _audioClipEvents;

        [Space, SerializeField]
        private AudioGroupEventData[] _audioClipGroupEvents;
        
        public AudioClipEventData[] AudioClipEvents => _audioClipEvents;
        public AudioGroupEventData[] AudioClipGroupEvents => _audioClipGroupEvents;
    }
}