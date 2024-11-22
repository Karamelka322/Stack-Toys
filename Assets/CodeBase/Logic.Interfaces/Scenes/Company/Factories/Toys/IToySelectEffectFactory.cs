using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Factories.Toys
{
    public interface IToySelectEffectFactory
    {
        UniTask<GameObject> SpawnAsync(Transform parent);
    }
}