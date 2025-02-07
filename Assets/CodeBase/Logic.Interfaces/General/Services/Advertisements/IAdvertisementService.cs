using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.General.Services.Advertisements
{
    public interface IAdvertisementService
    {
        BoolReactiveProperty IsShowing { get; }
        
        UniTask TryShowInterstitialAsync();
    }
}