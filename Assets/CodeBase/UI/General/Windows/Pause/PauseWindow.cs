using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Providers.Data.Saves;
using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.Interfaces.General.Services.Audio;
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
        private readonly ISettingsSaveDataProvider _settingsSaveDataProvider;
        private readonly IWindowService _windowService;
        private readonly IAudioService _audioService;

        private PauseWindowMediator _mediator;

        public PauseWindow(
            IWindowService windowService,
            IPauseWindowFactory pauseWindowFactory,
            ISettingsSaveDataProvider settingsSaveDataProvider,
            IAudioService audioService,
            ICompanySceneUnload companySceneUnload,
            ISceneLoadService sceneLoadService) : base(windowService)
        {
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
            
            _mediator.MenuButton.onClick.AddListener(OnMenuButtonClicked);
            _mediator.CloseButton.onClick.AddListener(OnCloseButtonClicked);
            _mediator.MusicVolumeToggle.onValueChanged.AddListener(OnMusicVolumeToggleClicked);
            _mediator.SoundVolumeToggle.onValueChanged.AddListener(OnSoundVolumeToggleClicked);

            _mediator.MusicVolumeToggle.isOn = _settingsSaveDataProvider.IsMusicVolumeMute() == false;
            _mediator.SoundVolumeToggle.isOn = _settingsSaveDataProvider.IsSoundsVolumeMute() == false;
        }

        public override void Close()
        {
            _mediator.MenuButton.onClick.RemoveListener(OnMenuButtonClicked);
            _mediator.CloseButton.onClick.RemoveListener(OnCloseButtonClicked);
            _mediator.MusicVolumeToggle.onValueChanged.RemoveListener(OnMusicVolumeToggleClicked);
            _mediator.SoundVolumeToggle.onValueChanged.RemoveListener(OnSoundVolumeToggleClicked);

            Object.Destroy(_mediator.gameObject);
        }

        private void OnCloseButtonClicked()
        {
            _windowService.CloseAsync<PauseWindow>().Forget();
        }

        private void OnMenuButtonClicked()
        {
            _companySceneUnload.Unload();
            _sceneLoadService.LoadScene(SceneNames.Menu);
        }

        private void OnMusicVolumeToggleClicked(bool isOn)
        {
            _audioService.SetMusicVolume(isOn ? 1f : 0f);
        }
        
        private void OnSoundVolumeToggleClicked(bool isOn)
        {
            _audioService.SetSoundsVolume(isOn ? 1f : 0f);
        }
    }
}