using System;
using Sirenix.OdinInspector;

namespace CodeBase.Data.Models.Audio
{
    [Serializable]
    public class AudioClipSettingData
    {
        [Required] 
        public string AddressableName;
        
        [PropertyRange(0f, 1f)]
        public float Volume = 1f;
        
        [PropertyRange(-3f, 3f)]
        public float Pitch = 1f;
    }
}