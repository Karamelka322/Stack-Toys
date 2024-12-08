using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Logic.General.Unity.Audio
{
    public class AudioListenerMediator : MonoBehaviour
    {
        [SerializeField, Required] 
        private Transform _musicSourceParent;
        
        [SerializeField, Required] 
        private Transform _soundsSourceParent;

        public Transform MusicSourceParent => _musicSourceParent;
        public Transform SoundsSourceParent => _soundsSourceParent;
    }
}