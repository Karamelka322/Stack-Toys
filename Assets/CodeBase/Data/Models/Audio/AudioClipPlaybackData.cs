using System.Threading;
using UnityEngine;

namespace CodeBase.Data.Models.Audio
{
    public class AudioClipPlaybackData
    {
        public string AddressableName;
        public AudioSource Source;
        public CancellationTokenSource CancellationTokenSource;
    }
}