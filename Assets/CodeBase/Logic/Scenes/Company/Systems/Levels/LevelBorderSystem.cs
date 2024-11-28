using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.Logic.Scenes.Company.Unity;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Levels
{
    public class LevelBorderSystem : ILevelBorderSystem, IDisposable
    {
        private const float TopBorder = 4f;
        
        private readonly IDisposable _disposable;

        private LevelMediator _level;

        public Vector3 OriginPoint { get; private set; }
        public Vector3 BottomLeftPoint { get; private set; }
        public Vector3 BottomRightPoint { get; private set; }
        public Vector3 TopLeftPoint { get; private set; }
        public Vector3 TopRightPoint { get; private set; }
        
        public LevelBorderSystem(ILevelProvider levelProvider)
        {
            _disposable = levelProvider.Level.Subscribe(OnSceneLoad);
        }

        private void OnSceneLoad(LevelMediator levelMediator)
        {
            if (levelMediator == null)
            {
                return;
            }
        
            _level = levelMediator;

            OriginPoint = _level.OriginPoint.position;
            
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
                _level.OriginPoint.position.y + max, (_level.OriginPoint.up * _level.Height).y - max + TopBorder);
            
            return clampPosition;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}