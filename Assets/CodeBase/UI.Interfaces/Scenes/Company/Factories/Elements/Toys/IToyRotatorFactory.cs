using CodeBase.UI.Scenes.Company.Mediators.Elements.Toys;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Interfaces.Scenes.Company.Factories.Elements.Toys
{
    public interface IToyRotatorFactory
    {
        UniTask<ToyRotatorMediator> SpawnAsync(Transform target);
    }
}