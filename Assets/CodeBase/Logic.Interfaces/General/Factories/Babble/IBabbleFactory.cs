using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Factories.Babble
{
    public interface IBabbleFactory
    {
        UniTask<GameObject> SpawnAsync(Vector3 position, Transform parent);
    }
}