using System;
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
            _interstitialModule = new InterstitialAdvertisementModule();
        }

        public void Dispose()
        {
            _interstitialModule?.Dispose();
            IsShowing?.Dispose();
        }

        public async UniTask ShowInterstitialAsync()
        {
            IsShowing.Value = true;
            
            await _interstitialModule.ShowAsync();
            
            IsShowing.Value = false;
        }
    }
}