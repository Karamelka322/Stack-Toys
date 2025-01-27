using System;
using System.Linq;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Observers.Toys
{
    public class ToyTowerHeightObserver : IToyTowerHeightObserver, IDisposable
    {
        private const float RayDistance = 5f;
        
        private readonly IDisposable _disposable;
        private readonly IToyTowerBuildObserver _towerBuildObserver;
        private readonly ILevelProvider _levelProvider;

        public ReactiveProperty<float> TowerHeight { get; } 
        
        public ToyTowerHeightObserver(IToyTowerBuildObserver towerBuildObserver, ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
            _towerBuildObserver = towerBuildObserver;
            
            TowerHeight = new ReactiveProperty<float>();

            towerBuildObserver.OnTowerFallen += OnTowerFallen;
            _disposable = towerBuildObserver.Tower.ObserveAdd().Subscribe(OnToyAdd);
        }

        public void Dispose()
        {
            _towerBuildObserver.OnTowerFallen -= OnTowerFallen;
            
            _disposable?.Dispose();
            TowerHeight?.Dispose();
        }
        
        private async void OnToyAdd(CollectionAddEvent<ToyMediator> addEvent)
        {
            TowerHeight.Value = await GetTowerHeightAsync();
        }

        private void OnTowerFallen()
        {
            TowerHeight.Value = 0;
        }

        private async UniTask<float> GetTowerHeightAsync()
        {
            if (_towerBuildObserver.Tower.Count == 0)
            {
                return 0f;
            }
            
            var toy = _towerBuildObserver.Tower.Last();
            var offset = Vector3.zero;
            var step = new Vector3(0, 0.05f, 0);
            var counter = 100;

            var rayOrigin = new Vector3()
            {
                x = (_levelProvider.Level.Value.OriginPoint.position + Vector3.left * RayDistance).x,
                y = toy.transform.position.y,
                z = toy.transform.position.z
            };

            for (int i = 0; i < counter; i++)
            {
                var direction = Vector3.right * RayDistance;
                var origin = rayOrigin + offset;
                
                // Debug.DrawRay(origin, direction, Color.cyan);
                
                if (Physics.Raycast(origin, direction))
                {
                    offset += step;
                }
                else
                {
                    return origin.y - _levelProvider.Level.Value.OriginPoint.position.y;
                }
            }
            
            Debug.LogError("Not calculate tower height");
            return 0;
        }
    }
}