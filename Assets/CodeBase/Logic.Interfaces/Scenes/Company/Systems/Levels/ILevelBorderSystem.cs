using CodeBase.Logic.General.Unity.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels
{
    public interface ILevelBorderSystem
    {
        Vector3 BottomLeftPoint { get; }
        Vector3 BottomRightPoint { get; }
        Vector3 TopLeftPoint { get; }
        Vector3 TopRightPoint { get; }
        Vector3 OriginPoint { get; }

        UniTask<float> GetHeightAsync();
        UniTask<Vector3> ClampAsync(ToyMediator toyMediator, Vector3 position);
    }
}