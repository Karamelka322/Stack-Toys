using CodeBase.Data.Constants;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Canvases;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.UI.Interfaces.Scenes.Company.Factories.Windows.Finish;
using CodeBase.UI.Scenes.Company.Mediators.Windows.Finish;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Scenes.Company.Factories.Windows.Finish
{
    public class CompanyFinishWindowFactory : ICompanyFinishWindowFactory
    {
        private readonly IAssetServices _assetServices;
        private readonly IWindowCanvasProvider _windowCanvasProvider;

        public CompanyFinishWindowFactory(IAssetServices assetServices, IWindowCanvasProvider windowCanvasProvider)
        {
            _windowCanvasProvider = windowCanvasProvider;
            _assetServices = assetServices;
        }

        public async UniTask<CompanyFinishWindowMediator> SpawnAsync()
        {
            var canvas = await _windowCanvasProvider.GetCanvasAsync();
            var prefab = await _assetServices.LoadAsync<GameObject>(AddressableConstants.CompanyScene.FinishWindow);
            var mediator = Object.Instantiate(prefab, canvas.transform).GetComponent<CompanyFinishWindowMediator>();

            return mediator;
        }
    }
}