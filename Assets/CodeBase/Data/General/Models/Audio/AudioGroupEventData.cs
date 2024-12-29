using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.General.Models.Audio
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