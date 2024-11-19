using CodeBase.Logic.General.Unity.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Factories.Toys
{
    public interface IToyFactory
    {
        UniTask<ToyMediator> SpawnAsync(Vector3 position);
    }
}