using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Scenes.Company.Windows.Main
{
    public class ToyRotatorElement : IDisposable
    {
        private const float _sliderDuration = 0.5f;
        
        private readonly IToyCountObserver _toyCountObserver;
        private readonly Slider _slider;
        private readonly CanvasGroup _canvasGroup;
        private readonly IToySelectObserver _toySelectObserver;
        private readonly IDisposable _disposable;

        public event Action<float> OnSliderChanged;

        public ToyRotatorElement(Slider slider, CanvasGroup canvasGroup, 
            IToyCountObserver toyCountObserver, IToySelectObserver toySelectObserver)
        {
            _canvasGroup = canvasGroup;
            _slider = slider;
            _toyCountObserver = toyCountObserver;
            
            _slider.onValueChanged.AddListener(OnSliderChangedInvoke);
            _disposable = toySelectObserver.Toy.Subscribe(OnSelectableToyChanged);

            HideSlider();
        }
        
        public class Factory : PlaceholderFactory<Slider, CanvasGroup, ToyRotatorElement> { }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public float GetSliderValue()
        {
            return _slider.value;
        }
        
        public Quaternion SliderValueToRotation(float value)
        {
            return Quaternion.Euler(0, 0, 360 * value);
        }
        
        private void OnSelectableToyChanged(ToyMediator toyMediator)
        {
            if (toyMediator == null)
            {
                if (_toyCountObserver.NumberOfTowerBuildToys.Value == 0)
                {
                    HideSlider(0);
                    SetRandomValueToSlider();
                }
                else
                {
                    HideSlider(_sliderDuration, SetRandomValueToSlider);
                }
            }
            else
            {
                ShowSlider();
            }
        }

        private void SetRandomValueToSlider()
        {
            _slider.value = UnityEngine.Random.Range(0f, 1f);
        }
        
        private void ShowSlider()
        {
            DOVirtual.Float(0f, 1f, _sliderDuration, (value) => _canvasGroup.alpha = value);
        }

        private void HideSlider(float duration = _sliderDuration, TweenCallback onComplete = null)
        {
            DOVirtual.Float(1f, 0f, duration, 
                (value) => _canvasGroup.alpha = value).OnComplete(onComplete);
        }

        private void OnSliderChangedInvoke(float value) => OnSliderChanged?.Invoke(value);
    }
}