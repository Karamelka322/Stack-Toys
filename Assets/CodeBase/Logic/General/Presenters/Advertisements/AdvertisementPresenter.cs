using System;
using CodeBase.Logic.General.Services.Advertisements;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.General.Presenters.Advertisements
{
    public class AdvertisementPresenter : IDisposable
    {
        private const float _time = 110f;
        
        private readonly IAdvertisementService _advertisementService;
        private readonly IDisposable _disposable;

        public AdvertisementPresenter(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;

            _disposable = Observable.Interval(TimeSpan.FromSeconds(_time)).Subscribe(OnTick);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnTick(long value)
        {
            if (_advertisementService.IsShowing.Value)
            {
                return;
            }
            
            _advertisementService.TryShowInterstitialAsync().Forget();
        }
    }
}