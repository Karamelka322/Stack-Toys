using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.General.Providers.ScriptableObjects.Cameras
{
    public interface ICameraSettingsProvider
    {
        UniTask<float> GetScrollingSpeedAsync();
    }
}