using CodeBase.Logic.General.Unity.Toys;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels
{
    public interface ILevelBorderSystem
    {
        BoolReactiveProperty IsReady { get; }
        
        Vector3 DownLeftPoint { get; }
        Vector3 DownRightPoint { get; }
        Vector3 UpLeftPoint { get; }
        Vector3 UpRightPoint { get; }
        Vector3 OriginPoint { get; }

        UniTask<Vector3> ClampAsync(ToyMediator toyMediator, Vector3 position);
    }
}