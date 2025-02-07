using System;
using CodeBase.Data.General.Constants;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.General.Services.Advertisements
{
    public class AdvertisementService : IAdvertisementService, IDisposable
    {
        private readonly InterstitialAdvertisementModule _interstitialModule;
        
        public BoolReactiveProperty IsShowing { get; }
        
        public AdvertisementService()
        {
            IsShowing = new BoolReactiveProperty();

            if (BuildConstants.Advertisements)
            {
                _interstitialModule = new InterstitialAdvertisementModule();
            }
        }

        public void Dispose()
        {
            _interstitialModule?.Dispose();
            IsShowing?.Dispose();
        }

        public async UniTask TryShowInterstitialAsync()
        {
            if(BuildConstants.Advertisements)
            {
                IsShowing.Value = true;
            
                await _interstitialModule.TryShowAsync();
            
                IsShowing.Value = false;
            }
        }
    }
}