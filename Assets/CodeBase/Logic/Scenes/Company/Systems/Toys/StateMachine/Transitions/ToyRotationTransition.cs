using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Commands;
using CodeBase.Logic.Interfaces.General.Services.Input;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.Transitions
{
    public class ToyRotationTransition : BaseTransition
    {
        private readonly ToyMediator _toyMediator;
        private readonly IInputService _inputService;
        private readonly IClickCommand _raycastCommand;
        private readonly IMainWindow _mainWindow;

        public ToyRotationTransition(ToyMediator toyMediator, IMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _toyMediator = toyMediator;
        }
        
        public class Factory : PlaceholderFactory<ToyMediator, ToyRotationTransition> { }

        public override void Enter()
        {
            _mainWindow.ToyRotatorElement.OnSliderChanged += OnSliderChanged;
        }

        public override void Exit()
        {
            _mainWindow.ToyRotatorElement.OnSliderChanged -= OnSliderChanged;
        }

        private void OnSliderChanged(float value)
        {
            IsCompleted.Value = true;
        }
    }
}