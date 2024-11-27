using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Cameras
{
    public interface ICameraBorderSystem
    {
        UniTask<Vector3> GetCameraStartPointAsync();
        UniTask<Vector3> GetCameraEndPointAsync();
        UniTask<float> GetInterpolationAsync();
    }
}