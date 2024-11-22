using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.General.Providers.ScriptableObjects.Cameras
{
    public interface ICameraSettingsProvider
    {
        UniTask<float> GetScrollingSpeedAsync();
    }
}