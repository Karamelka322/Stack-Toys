using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States
{
    public class ToyRotateState : BaseState
    {
        private const float MoveToStartRotationDuration = 0.2f;
        private const float MoveToRotationDuration = 0.01f;
        
        private readonly ToyMediator _toyMediator;
        private readonly IMainWindow _mainWindow;

        public ToyRotateState(ToyMediator toyMediator, IMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _toyMediator = toyMediator;
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