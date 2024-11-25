using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public interface IMenuLevelElementFactory
    {
        UniTask<CompletedMenuLevelElement> SpawnCompletedVariantAsync(int levelIndex, Transform parent);
        UniTask<OpenedMenuLevelElement> SpawnOpenedVariantAsync(int levelIndex, Transform parent);
        UniTask<ClosedMenuLevelElement> SpawnClosedVariantAsync(int levelIndex, Transform parent);
    }
}