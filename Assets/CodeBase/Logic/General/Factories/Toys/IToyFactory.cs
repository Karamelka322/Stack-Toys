using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.Unity.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Factories.Toys
{
    public interface IToyFactory
    {
        (ToyMediator, ToyStateMachine) Spawn(GameObject prefab, Transform parent, Vector3 position);
        UniTask<(ToyMediator, ToyStateMachine)> SpawnAsync(string addressableName, Transform parent, Vector3 position);
    }
}