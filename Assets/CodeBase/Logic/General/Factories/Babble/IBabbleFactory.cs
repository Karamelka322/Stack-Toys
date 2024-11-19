using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States
{
    public interface IBabbleFactory
    {
        UniTask<GameObject> SpawnAsync(Vector3 position, Transform parent);
    }
}