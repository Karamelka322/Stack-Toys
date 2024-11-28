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
        private readonly ICompanyMainWindow _companyMainWindow;

        public ToyRotationTransition(ToyMediator toyMediator, ICompanyMainWindow companyMainWindow)
        {
            _companyMainWindow = companyMainWindow;
            _toyMediator = toyMediator;
        }
        
        public class Factory : PlaceholderFactory<ToyMediator, ToyRotationTransition> { }

        public override void Enter()
        {
            _companyMainWindow.OnSliderChanged += OnSliderChanged;
        }

        public override void Exit()
        {
            _companyMainWindow.OnSliderChanged -= OnSliderChanged;
        }

        private void OnSliderChanged(float value)
        {
            IsCompleted.Value = true;
        }
    }
}