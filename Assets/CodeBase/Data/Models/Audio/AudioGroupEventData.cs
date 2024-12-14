using System;
using CodeBase.Data.Models.Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.ScriptableObjects.Audio
{
    [Serializable]
    public struct AudioGroupEventData
    {
        [Required] 
        public string GroupId;

        [Space, SerializeField] 
        public AudioClipSettingData[] AudioClips;
    }
}