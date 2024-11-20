using System;
using UnityEngine;

namespace CodeBase.Logic.General.Services.Input
{
    public class SwipeModule
    {
        private const int Smooth = 7;
        
        private readonly ClickModule _clickModule;

        private Camera _camera;
        private Vector3 _previewPosition;
        private Vector3 _direction;
        private int _touchId;
        
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
                
            _previewPosition = viewportPoint;
        }

        private void OnClick(Vector3 clickPosition)
        {
            var viewportPoint = GetCamera().ScreenToViewportPoint(clickPosition);
            
            var direction = viewportPoint - _previewPosition;
            _previewPosition = Vector3.Lerp(_previewPosition, viewportPoint, Time.deltaTime * Smooth);

            if (direction != Vector3.zero)
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