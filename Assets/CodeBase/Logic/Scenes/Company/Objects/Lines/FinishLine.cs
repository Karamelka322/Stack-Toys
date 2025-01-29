using System;
using System.Threading.Tasks;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Unity.Finish;
using CodeBase.Logic.Interfaces.General.Services.Localizations;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Finish
{
    public class FinishLine : IDisposable
    {
        private readonly ILocalizationService _localizationService;
        private readonly FinishLineMediator _mediator;

        private float _height;

        public FinishLine(FinishLineMediator mediator, ILocalizationService localizationService)
        {
            _mediator = mediator;
            _localizationService = localizationService;
            
            LocalizeTitleAsync().Forget();
            
            _localizationService.OnLocaleChanged += OnLocaleChanged;
        }

        public class Factory : PlaceholderFactory<FinishLineMediator, FinishLine> { }

        public void Dispose()
        {
            _localizationService.OnLocaleChanged -= OnLocaleChanged;
        }
        
        public async UniTask SetHeightAsync(float height)
        {
            var meters = await _localizationService.LocalizeAsync(LocalizationConstants.Meters);
            
            _height = height;
            _mediator.Height.text = $"{height} {meters}";
        }

        public Vector3 GetPosition()
        {
            return _mediator.transform.position;
        }

        private void OnLocaleChanged()
        {
            SetHeightAsync(_height).Forget();
            LocalizeTitleAsync().Forget();
        }

        private async UniTask LocalizeTitleAsync()
        {
            _mediator.Finish.text = await _localizationService.LocalizeAsync(LocalizationConstants.Finish);
        }
    }
}