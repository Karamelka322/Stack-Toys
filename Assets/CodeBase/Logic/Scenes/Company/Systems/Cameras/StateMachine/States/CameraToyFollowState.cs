using System;
using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.States
{
    public class CameraToyFollowState : BaseState
    {
        private const float MovementSpeed = 2;
        
        private readonly Camera _camera;
        private readonly IToySelectObserver _toySelectObserver;
        private readonly ILevelProvider _levelProvider;

        private IDisposable _disposable;
        private float _interpolation;

        public CameraToyFollowState(Camera camera, IToySelectObserver toySelectObserver, ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
            _toySelectObserver = toySelectObserver;
            _camera = camera;
        }

        public class Factory : PlaceholderFactory<Camera, CameraToyFollowState> { }

        public override void Enter()
        {
            _disposable = Observable.EveryUpdate().Subscribe(OnUpdate);
        }

        public override void Exit()
        {
            _disposable?.Dispose();
        }

        private void OnUpdate(long tick)
        {
            var startPosition = _levelProvider.Level.CameraStartPoint.position;
            var endPosition = _levelProvider.Level.CameraEndPoint.position;

            var maxDistance = Vector3.Distance(startPosition, endPosition);
            
            var toyClampPosition = _toySelectObserver.Toy.Value.transform.position;
            toyClampPosition.z = startPosition.z;
            toyClampPosition.x = startPosition.x;
            toyClampPosition.y = Mathf.Clamp(toyClampPosition.y, startPosition.y, endPosition.y);

            var distanceToToy = Vector3.Distance(startPosition, toyClampPosition);

            _interpolation = Mathf.Lerp(_interpolation, distanceToToy / maxDistance, Time.deltaTime * MovementSpeed);
            
            _camera.transform.position = Vector3.Lerp(startPosition, endPosition, _interpolation);
        }
    }
}