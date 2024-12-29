using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
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