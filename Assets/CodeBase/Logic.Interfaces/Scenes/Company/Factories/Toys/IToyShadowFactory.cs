using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Factories.Toys
{
    public interface IToyShadowFactory
    {
        UniTask<GameObject> SpawnAsync();
    }
}