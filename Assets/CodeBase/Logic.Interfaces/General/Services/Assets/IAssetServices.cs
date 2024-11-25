using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Logic.Interfaces.General.Services.Assets
{
    public interface IAssetServices
    {
        /// <summary>
        /// Загрузить ассет по адрессабл имени
        /// </summary>
        /// <param name="addressableName">Имя ассета из адрессаблов</param>
        /// <typeparam name="TAsset">Некий тип</typeparam>
        UniTask<TAsset> LoadAsync<TAsset>(string addressableName) where TAsset : class;

        /// <summary>
        /// Загрузить ассет по адрессабл имени
        /// </summary>
        /// <param name="assetReferenceGameObject">Ассет референс</param>
        UniTask<GameObject> LoadAsync(AssetReferenceGameObject assetReferenceGameObject);

        /// <summary>
        /// Загрузить ассет по адрессабл имени
        /// </summary>
        /// <param name="assetReferenceGameObjects">Массив ассут референсов</param>
        UniTask<GameObject[]> LoadAsync(AssetReferenceGameObject[] assetReferenceGameObjects);
    }
}