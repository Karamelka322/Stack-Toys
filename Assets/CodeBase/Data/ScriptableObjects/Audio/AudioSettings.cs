using System;
using CodeBase.Data.Constants;
using CodeBase.Data.Models.Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.ScriptableObjects.Audio
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

    [Serializable]
    public struct AudioGroupEventData
    {
        [Required] 
        public string GroupId;

        [Space, SerializeField] 
        public AudioClipSettingData[] AudioClips;
    }
}