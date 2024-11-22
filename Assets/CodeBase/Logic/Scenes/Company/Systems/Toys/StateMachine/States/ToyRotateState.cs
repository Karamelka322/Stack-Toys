using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using CodeBase.UI.Scenes.Company.Windows.Main;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States
{
    public class ToyRotateState : BaseState
    {
        private const float MoveToStartRotationDuration = 0.2f;
        
        private readonly ToyMediator _toyMediator;
        private readonly ICompanyMainWindow _companyMainWindow;

        public ToyRotateState(ToyMediator toyMediator, ICompanyMainWindow companyMainWindow)
        {
            _companyMainWindow = companyMainWindow;
            _toyMediator = toyMediator;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyRotateState> { }

        public override void Enter()
        {
            var sliderValue = _companyMainWindow.GetSliderValue();
            var sliderValueToRotation = SliderValueToRotation(sliderValue);
            
            _toyMediator.transform.DOKill();
            _toyMediator.transform.DORotate(sliderValueToRotation.eulerAngles, MoveToStartRotationDuration);
            
            _companyMainWindow.OnSliderChanged += OnSliderChanged;
        }

        public override void Exit()
        {
            _companyMainWindow.OnSliderChanged -= OnSliderChanged;
        }

        private void OnSliderChanged(float value)
        {
            _toyMediator.transform.rotation = SliderValueToRotation(value);
        }

        private Quaternion SliderValueToRotation(float value)
        {
            return Quaternion.Euler(0, 0, 360 * value);
        }
    }
}