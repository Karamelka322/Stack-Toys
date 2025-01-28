using System;
using UnityEngine;

namespace CodeBase.Logic.General.Services.Input.Modules
{
    public class SwipeModule
    {
        private const int Smooth = 7;
        private const float RangeForStartSwipe = 0.02f;
        
        private readonly ClickModule _clickModule;

        private Camera _camera;
        private Vector3 _previewPosition;
        private Vector3 _startClickViewportPosition;
        private Vector3 _direction;
        private int _touchId;
        private bool _isSwipe;
        
        public event Action<Vector3> OnSwipe;

        public SwipeModule()
        {
            _clickModule = new ClickModule();

            _clickModule.OnClickDown += OnClickDown;
            _clickModule.OnClick += OnClick;
            _clickModule.OnClickUp += OnClickUp;
        }

        ~SwipeModule()
        {
            _clickModule.OnClickDown -= OnClickDown;
            _clickModule.OnClick -= OnClick;
            _clickModule.OnClickUp -= OnClickUp;
        }

        private void OnClickDown(Vector3 clickPosition)
        {
            var viewportPoint = GetCamera().ScreenToViewportPoint(clickPosition);

            _isSwipe = false;
            _startClickViewportPosition = viewportPoint;
            _previewPosition = viewportPoint;
        }

        private void OnClick(Vector3 clickPosition)
        {
            var viewportPoint = GetCamera().ScreenToViewportPoint(clickPosition);
            
            var direction = viewportPoint - _previewPosition;
            _previewPosition = Vector3.Lerp(_previewPosition, viewportPoint, Time.deltaTime * Smooth);
            
            if (_isSwipe == false && Vector3.Distance(_startClickViewportPosition, viewportPoint) > RangeForStartSwipe)
            {
                _isSwipe = true;
            }

            if (_isSwipe)
            {
                OnSwipe?.Invoke(direction);
            }
        }

        private void OnClickUp(Vector3 clickPosition)
        {
            _previewPosition = Vector2.zero;
        }
        
        private Camera GetCamera()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            return _camera;
        }

    }
}