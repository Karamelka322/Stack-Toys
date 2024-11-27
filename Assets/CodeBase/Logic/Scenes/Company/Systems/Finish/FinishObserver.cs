using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Finish
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
            var direction = _levelBorderSystem.TopRightPoint - _levelBorderSystem.TopLeftPoint;
            var ray = new Ray(_levelBorderSystem.TopLeftPoint, direction);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider == toyMediator.Collider)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}