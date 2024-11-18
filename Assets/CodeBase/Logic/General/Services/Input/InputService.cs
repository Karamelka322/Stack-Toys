using System;
using UnityEngine;

namespace CodeBase.Logic.General.Services.Input
{
    public class InputService : IInputService, IDisposable
    {
        private readonly ClickModule _clickModule;

        public event Action<Vector3> OnClickDown;
        public event Action<Vector3> OnClick;
        public event Action<Vector3> OnClickUp;

        public InputService()
        {
            _clickModule = new ClickModule();
            
            _clickModule.OnClickDown += OnClickDownInvoke;
            _clickModule.OnClick += OnClickInvoke;
            _clickModule.OnClickUp += OnClickUpInvoke;
        }

        public void Dispose()
        {
            _clickModule.OnClickDown -= OnClickDownInvoke;
            _clickModule.OnClick -= OnClickInvoke;
            _clickModule.OnClickUp -= OnClickUpInvoke;
        }

        private void OnClickDownInvoke(Vector3 mousePosition) => OnClickDown?.Invoke(mousePosition);
        private void OnClickInvoke(Vector3 mousePosition) => OnClick?.Invoke(mousePosition);
        private void OnClickUpInvoke(Vector3 mousePosition) => OnClickUp?.Invoke(mousePosition);
    }
}