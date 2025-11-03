using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
// using YandexMobileAds;
// using YandexMobileAds.Base;

namespace CodeBase.Logic.General.Services.Advertisements
{
    public class InterstitialAdvertisementModule : IDisposable
    {
        private const string InterstitialId = "R-M-14000591-1";
        
        // private readonly InterstitialAdLoader _loader;
        private readonly CancellationTokenSource _cancellationTokenSource;

        // private Interstitial _interstitial;

        public BoolReactiveProperty IsPreloaded { get; }
        public BoolReactiveProperty IsShowing { get; }
        
        public InterstitialAdvertisementModule()
        {
            IsPreloaded = new BoolReactiveProperty();
            IsShowing = new BoolReactiveProperty();

            _cancellationTokenSource = new CancellationTokenSource();
            // _loader = new InterstitialAdLoader();
            
            // _loader.OnAdLoaded += OnLoaded;
            // _loader.OnAdFailedToLoad += OnFailedToLoad;

            Preload();
        }
        
        public void Dispose()
        {
            // Destroy();
            
            _cancellationTokenSource?.Cancel();
            IsShowing?.Dispose();
            
            // _loader.OnAdLoaded -= OnLoaded;
            // _loader.OnAdFailedToLoad -= OnFailedToLoad;
        }

        public async UniTask TryShowAsync()
        {
            if (Application.isEditor)
            {
                return;
            }
            
            try
            {
                await UniTask.WaitWhile(() => IsPreloaded.Value == false,
                    cancellationToken: _cancellationTokenSource.Token);
                
                // if (_interstitial != null)
                // {
                //     _interstitial.Show();
                // }
                
                await UniTask.WaitWhile(() => IsShowing.Value,
                    cancellationToken: _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException exception) { }
        }

        private void Preload()
        {
            if (IsPreloaded.Value)
            {
                return;
            }
            
            // var adRequestConfiguration = new AdRequestConfiguration
            //     .Builder(InterstitialId).Build();
            //
            // _loader.LoadAd(adRequestConfiguration);
        }
        //
        // private void OnLoaded(object sender, InterstitialAdLoadedEventArgs args)
        // {
        //     _interstitial = args.Interstitial;
        //     
        //     _interstitial.OnAdShown += OnShown;
        //     _interstitial.OnAdFailedToShow += OnFailedToShow;
        //     _interstitial.OnAdDismissed += OnDismissed;
        //
        //     IsPreloaded.Value = true;
        // }
        //
        // private void OnFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        // {
        //     UnityEngine.Debug.LogError("Interstitial Advertisement Failed To Load. " + args.Message);
        // }
        //
        // private void OnShown(object sender, EventArgs args)
        // {
        //     IsShowing.Value = true;
        // }
        //
        // private void OnFailedToShow(object sender, AdFailureEventArgs args)
        // {
        //     IsShowing.Value = false;
        //     IsPreloaded.Value = false;
        //     
        //     Destroy();
        //     Preload();
        //     
        //     UnityEngine.Debug.LogError("Interstitial Advertisement Failed To Show. " + args.Message);
        // }
        //
        // private void OnDismissed(object sender, EventArgs args)
        // {
        //     IsShowing.Value = false;
        //     IsPreloaded.Value = false;
        //     
        //     Destroy();
        //     Preload();
        // }
        //
        // private void Destroy()
        // {
        //     if (_interstitial == null)
        //         return;
        //     
        //     _interstitial.OnAdShown -= OnShown;
        //     _interstitial.OnAdFailedToShow -= OnFailedToShow;
        //     _interstitial.OnAdDismissed -= OnDismissed;
        //         
        //     _interstitial.Destroy();
        //     _interstitial = null;
        // }
    }
}