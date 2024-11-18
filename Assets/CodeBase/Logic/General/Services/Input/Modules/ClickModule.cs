using System;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Services.Input
{
    public class ClickModule
    {
        private readonly IDisposable _disposable;
        
        private Camera _camera;
        private Vector3 _mousePosition;

        public event Action<Vector3> OnClickDown;
        public event Action<Vector3> OnClick;
        public event Action<Vector3> OnClickUp;
        
        public ClickModule()
        {
            _disposable = Observable.EveryUpdate().Subscribe(OnUpdate);
        }

        ~ClickModule()
        {
            _disposable?.Dispose();
        }

        private void OnUpdate(long tick)
        {
            if (TryClickDown(out _mousePosition))
            {
                OnClickDown?.Invoke(_mousePosition);
            }
            
            if (TryClick(out _mousePosition))
            {
                OnClick?.Invoke(_mousePosition);
            }
            
            if (TryClickUp(out _mousePosition))
            {
                OnClickUp?.Invoke(_mousePosition);
            }
        }

        private static bool TryClickDown(out Vector3 mousePosition)
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                mousePosition = UnityEngine.Input.mousePosition;
                return true;
            }
            
            mousePosition = Vector3.zero;
            return false;
        }
        
        private static bool TryClick(out Vector3 mousePosition)
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                mousePosition = UnityEngine.Input.mousePosition;
                return true;
            }
            
            mousePosition = Vector3.zero;
            return false;
        }
        
        private static bool TryClickUp(out Vector3 mousePosition)
        {
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                mousePosition = UnityEngine.Input.mousePosition;
                return true;
            }
            
            mousePosition = Vector3.zero;
            return false;
        }
    }
}