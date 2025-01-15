using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Menu.Systems
{
    public class MenuSceneEnvironmentLoader : IMenuSceneEnvironmentLoader
    {
        private readonly ICompanyLevelsSettingProvider _companyLevelsSettingProvider;

        public BoolReactiveProperty IsLoaded { get; }
        
        public MenuSceneEnvironmentLoader(ICompanyLevelsSettingProvider companyLevelsSettingProvider, IAssetService assetService)
        {
            _companyLevelsSettingProvider = companyLevelsSettingProvider;
            IsLoaded = new BoolReactiveProperty();

            LoadAsync().Forget();
        }

        private async UniTask LoadAsync()
        {
            var prefab = await _companyLevelsSettingProvider.GetLevelPrefabAsync(0);
            Object.Instantiate(prefab);

            IsLoaded.Value = true;
        }
    }
}