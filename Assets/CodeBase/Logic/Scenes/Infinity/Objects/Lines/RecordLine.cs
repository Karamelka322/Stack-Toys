using System;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Services.Localizations;
using CodeBase.Logic.Scenes.Infinity.Unity.Lines;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Infinity.Objects.Lines
{
    public class RecordLine : IDisposable
    {
        private readonly ILocalizationService _localizationService;
        private readonly RecordLineMediator _mediator;

        private string _titleLocalizationId;
        private float _heightValue;
        
        private Sequence _sequence;
        
        public RecordLine(RecordLineMediator mediator, ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _mediator = mediator;

            _localizationService.OnLocaleChanged += OnLocaleChanged;
        }
        
        public class Factory : PlaceholderFactory<RecordLineMediator, RecordLine> { }

        public void Dispose()
        {
            _localizationService.OnLocaleChanged -= OnLocaleChanged;
        }

        public async UniTask SetTitleAsync(string localizationId)
        {
            _titleLocalizationId = localizationId;
            _mediator.Title.text = await _localizationService.LocalizeAsync(localizationId);
        }

        public void ShowTitle()
        {
            _mediator.Title.alpha = 1f;
        }
        
        public void HideTitle()
        {
            if (_mediator.Title.alpha == 0f)
            {
                return;
            }
            
            DOVirtual.Float(1f, 0f, 1f, alpha => _mediator.Title.alpha = alpha);
        }
        
        public async UniTask SetHeightAsync(float heightValue)
        {
            var heightText = await _localizationService.LocalizeAsync(LocalizationConstants.Meters);
            
            _heightValue = heightValue;
            _mediator.Height.text = $"{heightValue}{heightText}";
        }

        public void ShowHeight()
        {
            _mediator.Height.alpha = 1f;
        }
        
        public void HideHeight()
        {
            if (_mediator.Height.alpha == 0f)
            {
                return;
            }
            
            DOVirtual.Float(1f, 0f, 1f, alpha => _mediator.Height.alpha = alpha);
        }
        
        public void SetLineAlpha(float alpha)
        {
            var color = _mediator.Line.color;
            color.a = alpha;
            
            _mediator.Line.color = color;
        }

        public void PlayShow()
        {
            DOVirtual.Float(0f, 1f, 1f, alpha =>
            {
                _mediator.Title.alpha = alpha;
                _mediator.Height.alpha = alpha;
                
                SetLineAlpha(alpha);
            });
        }
        
        public void PlayHide()
        {
            DOVirtual.Float(1f, 0f, 1f, alpha =>
            {
                _mediator.Title.alpha = alpha;
                _mediator.Height.alpha = alpha;
                
                SetLineAlpha(alpha);
            });
        }
        
        public void PlayImpulse()
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(DOVirtual.Float(1f, 0.3f, 0.5f, alpha =>
            {
                _mediator.Title.alpha = _mediator.Title.alpha > 0 ? alpha : 0;
                _mediator.Height.alpha = _mediator.Height.alpha > 0 ? alpha : 0;
                
                if (_mediator.Line.color.a > 0)
                {
                    SetLineAlpha(alpha);
                }
            }));
            
            _sequence.Append(DOVirtual.Float(0.3f, 1f, 0.5f, alpha =>
            {
                _mediator.Title.alpha = _mediator.Title.alpha > 0 ? alpha : 0;
                _mediator.Height.alpha = _mediator.Height.alpha > 0 ? alpha : 0;

                if (_mediator.Line.color.a > 0)
                {
                    SetLineAlpha(alpha);
                }
            }));
            
            _sequence.SetLoops(-1);
            _sequence.Play();
        }

        public async UniTask StopImpulseAsync()
        {
            if (_sequence == null || _sequence.active == false)
            {
                return;
            }
            
            await _sequence.AsyncWaitForElapsedLoops(1);
            _sequence.Kill(true);
        }

        public void SetPosition(Vector3 position)
        {
            DOVirtual.Vector3(_mediator.transform.position, position, 1f, point =>
            {
                _mediator.transform.position = point;
            });
        }


        public Vector3 GetPosition()
        {
            return _mediator.transform.position;
        }
        private void OnLocaleChanged()
        {
            SetTitleAsync(_titleLocalizationId).Forget();
            SetHeightAsync(_heightValue).Forget();
        }
    }
}