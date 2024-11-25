using CodeBase.Logic.General.Services.Windows;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using CodeBase.UI.General.Mediators.Windows.Pause;
using CodeBase.UI.Interfaces.General.Factories.Windows.Pause;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.General.Windows.Pause
{
    public class PauseWindow : BaseWindow
    {
        private readonly IPauseWindowFactory _pauseWindowFactory;
        
        private PauseWindowMediator _mediator;

        public PauseWindow(IWindowService windowService, IPauseWindowFactory pauseWindowFactory) : base(windowService)
        {
            _pauseWindowFactory = pauseWindowFactory;
        }

        public override async UniTask OpenAsync()
        {
            _mediator = await _pauseWindowFactory.SpawnAsync();
        }

        public override void Close()
        {
            Object.Destroy(_mediator.gameObject);
        }
    }
}