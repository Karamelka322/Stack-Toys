using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Providers.Data.ScriptableObjects.Cameras
{
    public interface ICameraSettingsProvider
    {
        UniTask<float> GetScrollingSpeedAsync();
        UniTask<Vector3> GetOffsetAsync();
    }
}