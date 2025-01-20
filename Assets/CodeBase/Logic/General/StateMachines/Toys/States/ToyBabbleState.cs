using CodeBase.Logic.General.StateMachines.Core;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Systems.Toys;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using DG.Tweening;
using Zenject;

namespace CodeBase.Logic.General.StateMachines.Toys.States
{
    public class ToyBabbleState : BaseState
    {
        private readonly ToyMediator _toyMediator;
        private readonly IToyBabbleSystem _toyBabbleSystem;
        private readonly IToyRotateAnimation _toyRotateAnimation;
        private readonly IMainWindow _mainWindow;

        public ToyBabbleState(ToyMediator toyMediator, IToyBabbleSystem toyBabbleSystem,
            IToyRotateAnimation toyRotateAnimation, IMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _toyRotateAnimation = toyRotateAnimation;
            _toyBabbleSystem = toyBabbleSystem;
            _toyMediator = toyMediator;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyBabbleState> { }
        
        public override async void Enter()
        {   
            await _toyBabbleSystem.AddAsync(_toyMediator);
            _toyRotateAnimation.Play(_toyMediator);
        }
    
        public override void Exit()
        {
            _toyBabbleSystem.Remove(_toyMediator);
            _toyRotateAnimation.Stop(_toyMediator);
            
            var sliderValue = _mainWindow.ToyRotatorElement.GetSliderValue();
            var sliderValueToRotation = _mainWindow.ToyRotatorElement.SliderValueToRotation(sliderValue);

            _toyMediator.transform.DORotate(sliderValueToRotation.eulerAngles, 0.4f);
        }
    }
}