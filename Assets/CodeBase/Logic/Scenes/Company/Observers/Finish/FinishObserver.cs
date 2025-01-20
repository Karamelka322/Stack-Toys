using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Observers.Finish
{
    public class FinishObserver : IFinishObserver, IDisposable
    {
        private readonly ILevelBorderSystem _levelBorderSystem;
        private readonly IDisposable _disposable;

        public BoolReactiveProperty IsFinished { get; }

        public FinishObserver(IToyTowerObserver towerObserver, ILevelBorderSystem levelBorderSystem)
        {
            _levelBorderSystem = levelBorderSystem;
            
            IsFinished = new BoolReactiveProperty();

            _disposable = towerObserver.Tower.ObserveAdd().Subscribe(OnAddToy);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            IsFinished?.Dispose();
        }

        private void OnAddToy(CollectionAddEvent<ToyMediator> addEvent)
        {
            if (IsFinishToy(addEvent.Value))
            {
                IsFinished.Value = true;
            }
        }

        private bool IsFinishToy(ToyMediator toyMediator)
        {
            var direction = _levelBorderSystem.UpRightPoint - _levelBorderSystem.UpLeftPoint;
            var ray = new Ray(_levelBorderSystem.UpLeftPoint, direction);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                foreach (var collider in toyMediator.Colliders)
                {
                    if (hit.collider == collider)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}