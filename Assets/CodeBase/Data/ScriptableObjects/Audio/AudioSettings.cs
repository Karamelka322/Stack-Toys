using CodeBase.Data.Constants;
using CodeBase.Data.Models.Audio;
using UnityEngine;

namespace CodeBase.Data.ScriptableObjects.Audio
{
    [CreateAssetMenu(menuName = AssetMenuConstants.ScriptableObjects + nameof(AudioSettings), fileName = nameof(AudioSettings))]
    public class AudioSettings : ScriptableObject
    {
        [SerializeField]
        private AudioVolumeData[] _compositions;

        public AudioVolumeData[] Compositions => _compositions;
    }
}