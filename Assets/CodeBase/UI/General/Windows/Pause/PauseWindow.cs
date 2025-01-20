using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Providers.Data.Saves;
using CodeBase.Logic.General.Services.Localizations;
using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Services.Assets;
using CodeBase.Logic.Interfaces.General.Services.Audio;
using CodeBase.Logic.Interfaces.General.Services.Localizations;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.UI.General.Mediators.Windows.Pause;
using CodeBase.UI.Interfaces.General.Factories.Windows.Pause;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.General.Windows.Pause
{
    public class PauseWindow : BaseWindow
    {
        private readonly IPauseWindowFactory _pauseWindowFactory;
        private readonly ICompanySceneUnload _companySceneUnload;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IAudioSaveDataProvider _settingsSaveDataProvider;
        private readonly ILocalizationService _localizationService;
        private readonly IWindowService _windowService;
        private readonly IAudioService _audioService;
        private readonly IAssetService _assetService;

        private PauseWindowMediator _mediator;

        public PauseWindow(
            IWindowService windowService,
            IPauseWindowFactory pauseWindowFactory,
            ILocalizationService localizationService,
            IAudioSaveDataProvider settingsSaveDataProvider,
            IAssetService assetService,
            IAudioService audioService,
            ICompanySceneUnload companySceneUnload,
            ISceneLoadService sceneLoadService) : base(windowService)
        {
            _assetService = assetService;
            _localizationService = localizationService;
            _settingsSaveDataProvider = settingsSaveDataProvider;
            _audioService = audioService;
            _windowService = windowService;
            _sceneLoadService = sceneLoadService;
            _companySceneUnload = companySceneUnload;
            _pauseWindowFactory = pauseWindowFactory;
        }

        public override async UniTask OpenAsync()
        {
            _mediator = await _pauseWindowFactory.SpawnAsync();
            
            _mediator.MenuButton.onClick.AddListener(OnMenuButtonClick);
            _mediator.CloseButton.onClick.AddListener(OnCloseButtonClick);
            _mediator.AdsButton.onClick.AddListener(OnAdsButtonClick);
            _mediator.LanguageButton.onClick.AddListener(OnLanguageButtonClick);
            _mediator.MusicVolumeToggle.onValueChanged.AddListener(OnMusicVolumeToggleClick);
            _mediator.SoundVolumeToggle.onValueChanged.AddListener(OnSoundVolumeToggleClick);

            _mediator.MusicVolumeToggle.isOn = _settingsSaveDataProvider.IsMusicVolumeMute() == false;
            _mediator.SoundVolumeToggle.isOn = _settingsSaveDataProvider.IsSoundsVolumeMute() == false;

            await LocalizeTextAsync();
            await LocalizeSpriteAsync();

            _localizationService.OnLocaleChanged += OnLocaleChanged;
        }
        
        public override void Close()
        {
            _mediator.MenuButton.onClick.RemoveListener(OnMenuButtonClick);
            _mediator.CloseButton.onClick.RemoveListener(OnCloseButtonClick);
            _mediator.AdsButton.onClick.RemoveListener(OnAdsButtonClick);
            _mediator.LanguageButton.onClick.RemoveListener(OnLanguageButtonClick);
            _mediator.MusicVolumeToggle.onValueChanged.RemoveListener(OnMusicVolumeToggleClick);
            _mediator.SoundVolumeToggle.onValueChanged.RemoveListener(OnSoundVolumeToggleClick);

            _localizationService.OnLocaleChanged -= OnLocaleChanged;
            
            Object.Destroy(_mediator.gameObject);
        }

        private void OnCloseButtonClick()
        {
            _windowService.CloseAsync<PauseWindow>().Forget();
        }

        private void OnMenuButtonClick()
        {
            _localizationService.OnLocaleChanged -= OnLocaleChanged;
            
            _companySceneUnload.Unload();
            _sceneLoadService.LoadSceneAsync(SceneNames.Menu, 1f).Forget();
        }

        private void OnAdsButtonClick()
        {
            // 
        }

        private void OnMusicVolumeToggleClick(bool isOn)
        {
            _audioService.SetMusicVolume(isOn ? 1f : 0f);
        }

        private void OnSoundVolumeToggleClick(bool isOn)
        {
            _audioService.SetSoundsVolume(isOn ? 1f : 0f);
        }
        
        private async void OnLanguageButtonClick()
        {
            var localeName = await _localizationService.GetLocaleAsync();
            
            if (localeName == LocalizationConstants.EnglishLocal)
            {
                _localizationService.SetLocaleAsync(LocalizationConstants.RussianLocal);
            }
            else
            {
                _localizationService.SetLocaleAsync(LocalizationConstants.EnglishLocal);
            }
        }

        private void OnLocaleChanged()
        {
            LocalizeTextAsync().Forget();
            LocalizeSpriteAsync().Forget();
        }

        private async UniTask LocalizeTextAsync()
        {
            _mediator.MusicLabel.text = await _localizationService.LocalizeAsync(LocalizationConstants.MusicKey);
            _mediator.SoundsLabel.text = await _localizationService.LocalizeAsync(LocalizationConstants.SoundsKey);
            _mediator.LocalizationLabel.text = await _localizationService.LocalizeAsync(LocalizationConstants.LanguageKey);
        }

        private async UniTask LocalizeSpriteAsync()
        {
            var localeName = await _localizationService.GetLocaleAsync();
            Sprite flagSprite = null;
            
            if (localeName == LocalizationConstants.EnglishLocal)
            {
                flagSprite = await _assetService.LoadAsync<Sprite>(AddressableConstants.EnglishFlag);
            }
            else
            {
                flagSprite = await _assetService.LoadAsync<Sprite>(AddressableConstants.RussianFlag);
            }
            
            _mediator.LanguageImage.sprite = flagSprite;
        }
    }
}