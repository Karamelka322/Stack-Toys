using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Canvases;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Main;
using CodeBase.UI.Scenes.Company.Mediators.Windows.Main;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Company.Factories.Windows.Main
{
    public class CompanyMainWindowFactory : ICompanyMainWindowFactory
    {
        private readonly IAssetServices _assetServices;
        private readonly IWindowCanvasProvider _windowCanvasProvider;

        public CompanyMainWindowFactory(
            IAssetServices assetServices,
            IWindowCanvasProvider windowCanvasProvider)
        {
            _windowCanvasProvider = windowCanvasProvider;
            _assetServices = assetServices;
        }

        public async UniTask<CompanyMainWindowMediator> SpawnAsync()
        {
            var canvas = await _windowCanvasProvider.GetCanvasAsync();
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableConstants.CompanyScene.MainWindow);
            var mediator = Object.Instantiate(prefab, canvas.transform).GetComponent<CompanyMainWindowMediator>();
            
            return mediator;
        }
    }
}