using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Models.Audio
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