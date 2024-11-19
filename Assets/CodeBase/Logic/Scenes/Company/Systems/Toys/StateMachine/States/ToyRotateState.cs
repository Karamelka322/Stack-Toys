using CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.UI.Scenes.Company.Elements.Toys.Rotator;
using CodeBase.UI.Scenes.Company.Factories.Elements.Toys.Rotator;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine.States
{
    public class ToyRotateState : BaseState
    {
        private readonly ToyMediator _toyMediator;
        private readonly ToyRotatorElement.Factory _toyRotatorElementFactory;
        private readonly IToyRotatorFactory _toyRotatorFactory;

        public ToyRotateState(ToyMediator toyMediator, IToyRotatorFactory toyRotatorFactory, ToyRotatorElement.Factory toyRotatorElementFactory)
        {
            _toyRotatorFactory = toyRotatorFactory;
            _toyRotatorElementFactory = toyRotatorElementFactory;
            _toyMediator = toyMediator;
        }

        public class Factory : PlaceholderFactory<ToyMediator, ToyRotateState> { }

        public override async void Enter()
        {
            var rotatorMediator = await _toyRotatorFactory.SpawnAsync(_toyMediator.transform);
            var rotatorElement = _toyRotatorElementFactory.Create(_toyMediator.transform, rotatorMediator);

            rotatorElement.OnInput += OnInput;
        }

        private void OnInput(Vector3 normalizedDirection)
        {
            var angleInRadians = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x);
            float angleInDegrees = (angleInRadians * Mathf.Rad2Deg + 360) % 360;
            
            _toyMediator.transform.rotation = Quaternion.Euler(0, 0, angleInDegrees - 180);
        }
    }
}