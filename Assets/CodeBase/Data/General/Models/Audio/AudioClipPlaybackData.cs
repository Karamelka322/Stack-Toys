using System.Threading;
using UnityEngine;

namespace CodeBase.Data.General.Models.Audio
{
    public class AudioClipPlaybackData
    {
        public string Id;
        public AudioSource Source;
        public CancellationTokenSource CancellationTokenSource;
    }
}