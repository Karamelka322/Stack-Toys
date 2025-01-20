using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using DG.Tweening;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys.States
{
    public class ToyRotateState : BaseState
    {
        private const float MoveToStartRotationDuration = 0.2f;
        private const float MoveToRotationDuration = 0.01f;
        
        private readonly ToyMediator _toyMediator;
        private readonly IMainWindow _mainWindow;
        // private readonly Vector3 _startScale;

        public ToyRotateState(ToyMediator toyMediator, IMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _toyMediator = toyMediator;
            // _startScale = toyMediator.transform.localScale;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyRotateState> { }

        public override void Enter()
        {
            var sliderValue = _mainWindow.ToyRotatorElement.GetSliderValue();
            Rotate(sliderValue, MoveToStartRotationDuration);
            
            _mainWindow.ToyRotatorElement.OnSliderChanged += OnSliderChanged;
        }

        public override void Exit()
        {
            _mainWindow.ToyRotatorElement.OnSliderChanged -= OnSliderChanged;
        }

        private void OnSliderChanged(float sliderValue)
        {
            Rotate(sliderValue, MoveToRotationDuration);
        }

        private void Rotate(float sliderValue, float duration)
        {
            var sliderValueToRotation = _mainWindow.ToyRotatorElement.SliderValueToRotation(sliderValue);
            
            _toyMediator.transform.DOKill();
            _toyMediator.transform.DORotate(sliderValueToRotation.eulerAngles, duration);
        }
    }
}