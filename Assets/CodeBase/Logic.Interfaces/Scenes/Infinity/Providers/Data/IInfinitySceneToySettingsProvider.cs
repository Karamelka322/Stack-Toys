using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Providers.Data
{
    public interface IInfinitySceneToySettingsProvider
    {
        UniTask<AssetReferenceGameObject[]> GetToysAsync();
    }
}