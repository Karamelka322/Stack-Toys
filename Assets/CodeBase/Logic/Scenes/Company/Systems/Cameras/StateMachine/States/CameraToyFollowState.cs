using CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes;
using CodeBase.Logic.General.Services.Input;
using CodeBase.Logic.Scenes.Company.Providers;
using CodeBase.Logic.Scenes.Company.Systems.Toys;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras.States
{
    public class CameraToyFollowState : BaseState
    {
        private const float MovementSpeed = 3;
        
        private readonly Camera _camera;
        private readonly IInputService _inputService;
        private readonly IToySelectObserver _toySelectObserver;
        private readonly ILevelProvider _levelProvider;

        private float _interpolation;

        public CameraToyFollowState(Camera camera, IInputService inputService,
            IToySelectObserver toySelectObserver, ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
            _toySelectObserver = toySelectObserver;
            _inputService = inputService;
            _camera = camera;
        }

        public class Factory : PlaceholderFactory<Camera, CameraToyFollowState> { }

        public override void Enter()
        {
            _inputService.OnSwipe += OnSwipe;
        }

        public override void Exit()
        {
            _inputService.OnSwipe -= OnSwipe;
        }

        private void OnSwipe(Vector3 direction)
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