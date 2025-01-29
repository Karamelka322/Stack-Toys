using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.General.Systems.Levels
{
    public interface ILevelSizeSystem
    {
        UniTask<float> GetHeightAsync();
        UniTask<float> GetWidthAsync();
    }
}