using CodeBase.UI.Scenes.Company.Elements.Toys.Rotator;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Company.Factories.Elements.Toys.Rotator
{
    public interface IToyRotatorFactory
    {
        UniTask<ToyRotatorMediator> SpawnAsync(Transform target);
    }
}