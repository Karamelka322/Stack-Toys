using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public interface IMenuLevelElementFactory
    {
        UniTask<MenuLevelElementMediator> SpawnCompletedVariantAsync(Transform parent);
        UniTask<MenuLevelElementMediator> SpawnOpenedVariantAsync(Transform parent);
        UniTask<MenuLevelElementMediator> SpawnClosedVariantAsync(Transform parent);
    }
}