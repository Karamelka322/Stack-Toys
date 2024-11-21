using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Providers;
using CodeBase.Logic.Scenes.Company.Systems.Load;
using CodeBase.Logic.Scenes.Company.Unity;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Level
{
    public class LevelBorderSystem : IDisposable, ILevelBorderSystem
    {
        private readonly ILevelProvider _levelProvider;
        private readonly IDisposable _disposable;

        private LevelMediator _level;

        public Vector3 BottomLeftPoint { get; private set; }
        public Vector3 BottomRightPoint { get; private set; }
        public Vector3 TopLeftPoint { get; private set; }
        public Vector3 TopRightPoint { get; private set; }
        
        public LevelBorderSystem(ICompanySceneLoad companySceneLoad, ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;

            _disposable = companySceneLoad.IsLoaded.Subscribe(OnSceneLoad);
        }

        private void OnSceneLoad(bool isLoaded)
        {
            if (isLoaded == false)
            {
                return;
            }
        
            _level = _levelProvider.Level;
            
            BottomLeftPoint = _level.OriginPoint.position - _level.OriginPoint.right * _level.Width / 2f;
            BottomRightPoint = _level.OriginPoint.position + _level.OriginPoint.right * _level.Width / 2f;
        
            TopLeftPoint = BottomLeftPoint + _level.OriginPoint.up * _level.Height;
            TopRightPoint = BottomRightPoint + _level.OriginPoint.up * _level.Height;
        }

        public Vector3 Clamp(ToyMediator toyMediator, Vector3 position)
        {
            var clampPosition = position;
            var size = toyMediator.MeshRenderer.bounds.size;
            var max = Mathf.Max(size.x, size.y) / 2f;

            clampPosition.x = Mathf.Clamp(clampPosition.x, BottomLeftPoint.x + max, BottomRightPoint.x - max);
            clampPosition.y = Mathf.Clamp(clampPosition.y, 
                _level.OriginPoint.position.y + max, (_level.OriginPoint.up * _level.Height).y - max + 5);
            
            return clampPosition;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}