using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Presenters.Toys
{
    public interface IToySelectEffectFactory
    {
        UniTask<GameObject> SpawnAsync(Transform parent);
    }
}