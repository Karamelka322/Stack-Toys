using CodeBase.Logic.General.Unity.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Factories.Babble
{
    public interface IBabbleFactory
    {
        UniTask<GameObject> SpawnAsync(Vector3 position, Transform parent);
        UniTask<GameObject> SpawnAsync(ToyMediator toyMediator);
    }
}