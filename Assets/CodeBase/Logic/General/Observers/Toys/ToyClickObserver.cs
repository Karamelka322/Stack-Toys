using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Commands;
using CodeBase.Logic.Interfaces.General.Services.Input;
using UnityEngine;

namespace CodeBase.Logic.General.StateMachines.Toys.Transitions
{
    public class ToyClickObserver : IToyClickObserver, IDisposable
    {
        private const float DistanceToSelect = 1.2f;
        
        private readonly IInputService _inputService;
        private readonly IClickCommand _clickCommand;
        private readonly Camera _camera;

        private RaycastHit _raycastHit;

        private readonly Dictionary<ToyMediator, Action> _observables;

        public ToyClickObserver(IInputService inputService, IClickCommand clickCommand)
        {
            _inputService = inputService;
            _clickCommand = clickCommand;
            _camera = Camera.main;
            
            _observables = new Dictionary<ToyMediator, Action>();
            
            _inputService.OnClickDown += OnClickDown;
        }

        public void Dispose()
        {
            _inputService.OnClickDown -= OnClickDown;
        }

        public void OnClickDownAsObservableAdd(ToyMediator toyMediator, Action action)
        {
            _observables.Add(toyMediator, action);
        }

        public void OnClickDownAsObservableRemove(ToyMediator toyMediator)
        {
            _observables.Remove(toyMediator);
        }

        private void OnClickDown(Vector3 mousePosition)
        {
            if (_observables.Count == 0)
            {
                return;
            }
            
            if (_clickCommand.HasUI(mousePosition))
            {
                return;
            }
            
            foreach (var observable in _observables.ToArray())
            {
                var screenToWorldPoint = _camera.ScreenToWorldPoint(mousePosition);
                screenToWorldPoint.z = observable.Key.transform.position.z;
            
                if (Vector3.Distance(screenToWorldPoint, observable.Key.transform.position) < DistanceToSelect)
                {
                    observable.Value?.Invoke();
                }
            }
        }
    }
}