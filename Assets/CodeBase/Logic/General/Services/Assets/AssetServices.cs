using System.Collections.Generic;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Logic.General.Services.Assets
{
    public class AssetServices : IAssetServices
    {
          private readonly Dictionary<string, AsyncOperationHandle> _cache = new(64);

        /// <summary>
        /// Загрузить ассет по адрессабл имени
        /// </summary>
        /// <param name="addressableName">Имя ассета из адрессаблов</param>
        /// <typeparam name="TAsset">Некий тип</typeparam>
        public async UniTask<TAsset> LoadAsync<TAsset>(string addressableName) where TAsset : class
        {
            if (HasCachedAsset(addressableName))
            {
                return await LoadCashedAssetAsync<TAsset>(addressableName);
            }

            return await LoadNewAssetAsync<TAsset>(addressableName);
        }
        
        /// <summary>
        /// Загрузить ассет по адрессабл имени
        /// </summary>
        /// <param name="assetReferenceGameObject">Ассет референс</param>
        public async UniTask<GameObject> LoadAsync(AssetReferenceGameObject assetReferenceGameObject)
        {
            if (HasCachedAsset(assetReferenceGameObject.AssetGUID))
            {
                return await LoadCashedAssetAsync<GameObject>(assetReferenceGameObject.AssetGUID);
            }

            return await LoadNewAssetAsync(assetReferenceGameObject);
        }
        
        /// <summary>
        /// Загрузить ассет по адрессабл имени
        /// </summary>
        /// <param name="assetReferenceGameObjects">Массив ассут референсов</param>
        public async UniTask<GameObject[]> LoadAsync(AssetReferenceGameObject[] assetReferenceGameObjects)
        {
            var tasks = new UniTask<GameObject>[assetReferenceGameObjects.Length];
            
            for (int i = 0; i < assetReferenceGameObjects.Length; i++)
            {
                if (HasCachedAsset(assetReferenceGameObjects[i].AssetGUID))
                {
                    tasks[i] = LoadCashedAssetAsync<GameObject>(assetReferenceGameObjects[i].AssetGUID);
                }
                else
                {
                    tasks[i] = LoadNewAssetAsync(assetReferenceGameObjects[i]);
                }
            }

            var group = new GameObject[tasks.Length];

            for (int i = 0; i < tasks.Length; i++)
            {
                group[i] = await tasks[i];
            }

            return group;
        }

        /// Загрузить новый ассет
        private async UniTask<TAsset> LoadNewAssetAsync<TAsset>(string addressableKey) where TAsset : class
        {
            var asyncOperationHandle = Addressables.LoadAssetAsync<TAsset>(addressableKey);
            _cache.Add(addressableKey, asyncOperationHandle);

            return await asyncOperationHandle;
        }

        /// Загрузить новый ассет
        private async UniTask<GameObject> LoadNewAssetAsync(AssetReferenceGameObject assetReferenceGameObject)
        {
            return await LoadNewAssetAsync<GameObject>(assetReferenceGameObject.AssetGUID);
        }

        /// Загрузить ассет из кеша
        private async UniTask<TAsset> LoadCashedAssetAsync<TAsset>(string addressableKey) where TAsset : class
        {
            var handler = _cache[addressableKey];

            if (handler.IsDone)
            {
                return handler.Result as TAsset;
            }
            
            await handler;
            
            return handler.Result as TAsset;
        }

        /// Удалить ассет из кеша
        public void ReleaseAsset(string addressableKey)
        {
            if (_cache.Remove(addressableKey, out var handle))
            {
                Addressables.Release(handle);
            }
        }

        /// Имеется ли ассет в кеше
        private bool HasCachedAsset(string addressableKey)
        {
            return _cache.ContainsKey(addressableKey);
        }
    }
}