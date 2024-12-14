using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Data.Models.Audio
{
    [Serializable]
    public class AudioClipEventData
    {
        [Required] 
        public string EventName;

        [SerializeField, HideLabel] 
        public AudioClipSettingData Settings;
    }

    [Serializable]
    public class AudioClipSettingData
    {
        [SerializeField, Required]
        public AssetReferenceT<AudioClip> AudioClip;
        
        [Space, PropertyRange(0f, 1f)]
        public float Volume = 1f;
        
        [PropertyRange(-3f, 3f)]
        public float Pitch = 1f;
    }
}