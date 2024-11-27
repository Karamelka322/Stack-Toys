using System;
using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Cameras;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using CodeBase.Logic.Scenes.Company.Systems.Levels;
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
        private readonly ICameraBorderSystem _cameraBorderSystem;

        private IDisposable _disposable;

        public CameraToyFollowState(
            Camera camera,
            IToySelectObserver toySelectObserver,
            ICameraBorderSystem cameraBorderSystem)
        {
            _cameraBorderSystem = cameraBorderSystem;
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

        private async void OnUpdate(long tick)
        {
            var startPosition = await _cameraBorderSystem.GetCameraStartPointAsync();
            var endPosition = await _cameraBorderSystem.GetCameraEndPointAsync();

            var maxDistance = startPosition != endPosition ? Vector3.Distance(startPosition, endPosition) : 1;
            
            var toyClampPosition = _toySelectObserver.Toy.Value.transform.position;
            toyClampPosition.z = startPosition.z;
            toyClampPosition.x = startPosition.x;
            toyClampPosition.y = Mathf.Clamp(toyClampPosition.y, startPosition.y, endPosition.y);

            var distanceToToy = Vector3.Distance(startPosition, toyClampPosition);

            var interpolation = await _cameraBorderSystem.GetInterpolationAsync();
            var nextInterpolation = Mathf.Lerp(interpolation, distanceToToy / maxDistance, Time.deltaTime * MovementSpeed);
            
            _camera.transform.position = Vector3.Lerp(startPosition, endPosition, nextInterpolation);
        }
    }
}