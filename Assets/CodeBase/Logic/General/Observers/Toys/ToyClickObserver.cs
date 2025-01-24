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
        private const float DistanceToSelect = 0.7f;
        
        private readonly IInputService _inputService;
        private readonly IClickFormulas _clickFormulas;
        private readonly Camera _camera;

        private RaycastHit _raycastHit;

        private readonly Dictionary<ToyMediator, Action> _observables;

        public ToyClickObserver(IInputService inputService, IClickFormulas clickFormulas)
        {
            _inputService = inputService;
            _clickFormulas = clickFormulas;
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
            
            if (_clickFormulas.HasUI(mousePosition))
            {
                return;
            }
            
            foreach (var observable in _observables.ToArray())
            {
                var screenToWorldPoint = _clickFormulas.ClickToWorldPosition(_camera,
                    mousePosition, observable.Key.transform);
                
                if (Vector3.Distance(screenToWorldPoint, observable.Key.transform.position) < DistanceToSelect)
                {
                    observable.Value?.Invoke();
                }
            }
        }
    }
}