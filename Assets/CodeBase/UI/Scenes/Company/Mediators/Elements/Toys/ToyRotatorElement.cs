using System;
using CodeBase.Logic.Interfaces.General.Commands;
using CodeBase.Logic.Interfaces.General.Services.Input;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Scenes.Company.Mediators.Elements.Toys
{
    public class ToyRotatorElement
    {
        private const float BigStickMoveSpeed = 0.4f;
        
        private readonly IDisposable _disposable;
        private readonly Camera _camera;
        private readonly Transform _target;
        private readonly ToyRotatorMediator _mediator;
        private readonly IInputService _inputService;
        private readonly IRaycastCommand _raycastCommand;

        private Vector2 _startStickAnchoredPosition;
        private bool _isStickSelected;

        public event Action<Vector3> OnInput;

        public ToyRotatorElement(
            Transform target,
            ToyRotatorMediator mediator,
            IInputService inputService,
            IRaycastCommand raycastCommand)
        {
            _raycastCommand = raycastCommand;
            _inputService = inputService;
            _mediator = mediator;
            _target = target;
            _camera = Camera.main;

            _inputService.OnClickDown += OnClickDown;
            _inputService.OnClick += OnClick;
            _inputService.OnClickUp += OnClickUp;
            
            _disposable = Observable.EveryUpdate().Subscribe(OnUpdate);
        }
        
        ~ToyRotatorElement()
        {
            _inputService.OnClickDown -= OnClickDown;
            _inputService.OnClick += OnClick;
            _inputService.OnClickUp -= OnClickUp;

            _disposable?.Dispose();
        }

        public class Factory :PlaceholderFactory<Transform, ToyRotatorMediator, ToyRotatorElement> { }

        private void OnUpdate(long tick)
        {
            _mediator.RectTransform.position = _camera.WorldToScreenPoint(_target.position);
        }
        
        private void OnClickDown(Vector3 clickPosition)
        {
            if (_raycastCommand.HasSelect(clickPosition, _mediator.BigStick.gameObject))
            {
                _isStickSelected = true;
                _startStickAnchoredPosition = _mediator.BigStick.anchoredPosition;
            }
        }
        
        private void OnClick(Vector3 clickPosition)
        {
            if (_isStickSelected)
            {
                var screenToWorldPoint = _camera.ScreenToWorldPoint(clickPosition);
                var worldToScreenPoint = _camera.WorldToScreenPoint(screenToWorldPoint);
                
                _mediator.BigStick.position = worldToScreenPoint;
                _mediator.SmallStick.anchoredPosition = _mediator.BigStick.anchoredPosition.normalized *
                                                        _startStickAnchoredPosition.magnitude;
                
                OnInput?.Invoke(_mediator.BigStick.anchoredPosition.normalized);
            }
        }
        
        private void OnClickUp(Vector3 clickPosition)
        {
            _isStickSelected = false;

            var nextPosition = _mediator.BigStick.anchoredPosition.normalized * _startStickAnchoredPosition.magnitude;
            var startPosition = _mediator.BigStick.anchoredPosition;

            DOVirtual.Vector3(startPosition, nextPosition, BigStickMoveSpeed, 
                value => _mediator.BigStick.anchoredPosition = value);
        }
    }
}