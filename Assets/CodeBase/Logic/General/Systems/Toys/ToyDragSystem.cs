using System;
using CodeBase.Logic.General.Services.Input;
using CodeBase.Logic.General.Unity.Toys;
using UnityEngine;

namespace CodeBase.Logic.General.Systems
{
    public class ToyDragSystem : IDisposable
    {
        private readonly IInputService _inputService;
        private readonly Camera _camera;

        private RaycastHit _raycastHit;
        private Transform _draggableObject;
        private Vector3 _draggableObjectPosition;

        public ToyDragSystem(IInputService inputService)
        {
            _camera = Camera.main;
            _raycastHit = new RaycastHit();
            _inputService = inputService;

            _inputService.OnClickDown += OnClickDown;
            _inputService.OnClick += OnClick;
            _inputService.OnClickUp += OnClickUp;
        }

        public void Dispose()
        {
            _inputService.OnClickDown -= OnClickDown;
            _inputService.OnClick -= OnClick;
            _inputService.OnClickUp -= OnClickUp;
        }

        private void OnClickDown(Vector3 mousePosition)
        {
            if (TrySelectToy(mousePosition, out var toy))
            {
                _draggableObject = toy.transform;
                _draggableObjectPosition = toy.transform.position;
            }
        }

        private void OnClick(Vector3 mousePosition)
        {
            if (_draggableObject == null)
            {
                return;
            }

            var ray = _camera.ScreenPointToRay(mousePosition);
            
            var distance = Vector3.Distance(ray.origin, _draggableObjectPosition);
            var nextPosition = ray.origin + ray.direction * distance;
            nextPosition.z = _draggableObjectPosition.z;
            
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
            
            _draggableObject.transform.position = nextPosition;
        }

        private void OnClickUp(Vector3 mousePosition)
        {
            _draggableObject = null;
        }
        
        private bool TrySelectToy(Vector3 mousePosition, out ToyMediator toyMediator)
        {
            var ray = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out _raycastHit))
            {
                return _raycastHit.transform.TryGetComponent(out toyMediator);
            }
            
            toyMediator = null;
            return false;
        }
    }
}