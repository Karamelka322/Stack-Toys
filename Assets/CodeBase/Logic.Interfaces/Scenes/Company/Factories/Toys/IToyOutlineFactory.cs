using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Presenters.Toys
{
    public interface IToyOutlineFactory
    {
        UniTask<GameObject> SpawnAsync();
    }
}