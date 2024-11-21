using System;
using UnityEngine;

namespace CodeBase.Logic.General.Services.Input
{
    public class InputService : IInputService, IDisposable
    {
        private readonly ClickModule _clickModule;
        private readonly SwipeModule _swipeModule;

        public event Action<Vector3> OnClickDown;
        public event Action<Vector3> OnClick;
        public event Action<Vector3> OnClickUp;
        public event Action<Vector3> OnSwipe;

        public bool IsClickPressed => _clickModule.IsClickPressed;
        
        public InputService()
        {
            _clickModule = new ClickModule();
            _swipeModule = new SwipeModule();
            
            _clickModule.OnClickDown += OnClickDownInvoke;
            _clickModule.OnClick += OnClickInvoke;
            _clickModule.OnClickUp += OnClickUpInvoke;
            _swipeModule.OnSwipe += OnSwipeInvoke;
        }

        public void Dispose()
        {
            _clickModule.OnClickDown -= OnClickDownInvoke;
            _clickModule.OnClick -= OnClickInvoke;
            _clickModule.OnClickUp -= OnClickUpInvoke;
            _swipeModule.OnSwipe -= OnSwipeInvoke;
        }

        private void OnClickDownInvoke(Vector3 mousePosition) => OnClickDown?.Invoke(mousePosition);
        private void OnClickInvoke(Vector3 mousePosition) => OnClick?.Invoke(mousePosition);
        private void OnClickUpInvoke(Vector3 mousePosition) => OnClickUp?.Invoke(mousePosition);
        private void OnSwipeInvoke(Vector3 obj) => OnSwipe?.Invoke(obj);
    }
}