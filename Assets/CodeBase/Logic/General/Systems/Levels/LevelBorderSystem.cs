using System;
using System.Linq;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Formulas;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Systems.Levels
{
    public class LevelBorderSystem : ILevelBorderSystem, IDisposable
    {
        private const float TopBorder = 4f;

        private readonly IEdgeFormulas _edgeFormulas;
        private readonly IRayFormulas _rayFormulas;
        private readonly IDisposable _disposable;
        private readonly ILevelSizeSystem _levelSizeSystem;

        private LevelMediator _level;

        public BoolReactiveProperty IsReady { get; }
        
        public Vector3 OriginPoint { get; private set; }
        public Vector3 DownLeftPoint { get; private set; }
        public Vector3 DownRightPoint { get; private set; }
        public Vector3 UpLeftPoint { get; private set; }
        public Vector3 UpRightPoint { get; private set; }
        
        public LevelBorderSystem(
            ILevelProvider levelProvider, 
            IRayFormulas rayFormulas,
            ILevelSizeSystem levelSizeSystem,
            IEdgeFormulas edgeFormulas)
        {
            _levelSizeSystem = levelSizeSystem;
            _edgeFormulas = edgeFormulas;
            _rayFormulas = rayFormulas;

            IsReady = new BoolReactiveProperty();
            
            _disposable = levelProvider.Level.Subscribe(OnLevelLoaded);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        public async UniTask<Vector3> ClampAsync(ToyMediator toyMediator, Vector3 position)
        {
            var size = toyMediator.MeshRenderer.bounds.size;
            var averageSize = Mathf.Max(size.x, size.y) / 2f;

            var height = await _levelSizeSystem.GetHeightAsync();
            var width = await _levelSizeSystem.GetWidthAsync();
            
            var (upLeftPoint, upRightPoint, downRightPoint, downLeftPoint) = 
                GetBorders(_level.OriginPoint, height + TopBorder, width, averageSize);
            
            upLeftPoint.y = Math.Max(upLeftPoint.y, upRightPoint.y);
            upRightPoint.y = Math.Max(upLeftPoint.y, upRightPoint.y);

#if UNITY_EDITOR
            
            Debug.DrawLine(UpLeftPoint, UpRightPoint, Color.red);
            Debug.DrawLine(UpRightPoint, DownRightPoint, Color.red);
            Debug.DrawLine(DownRightPoint, DownLeftPoint, Color.red);
            Debug.DrawLine(DownLeftPoint, UpLeftPoint, Color.red);
            Debug.DrawLine(upLeftPoint, upRightPoint, Color.yellow);
            Debug.DrawLine(upRightPoint, downRightPoint, Color.yellow);
            Debug.DrawLine(downRightPoint, downLeftPoint, Color.yellow);
            Debug.DrawLine(downLeftPoint, upLeftPoint, Color.yellow);

#endif
            
            if (HasLocatedWithinBorder(position, upLeftPoint, upRightPoint, downRightPoint, downLeftPoint))
            {
                return position;
            }
            
            return GetClosestPointByBorder(position, upLeftPoint, upRightPoint, downRightPoint, downLeftPoint);;
        }

        private async void OnLevelLoaded(LevelMediator levelMediator)
        {
            if (levelMediator == null)
            {
                return;
            }
        
            _level = levelMediator;

            OriginPoint = _level.OriginPoint.position;
            
            var height = await _levelSizeSystem.GetHeightAsync();
            var width = await _levelSizeSystem.GetWidthAsync();
            
            (UpLeftPoint, UpRightPoint, DownRightPoint, DownLeftPoint) = 
                GetBorders(_level.OriginPoint, height, width, 0f);

            IsReady.Value = true;
        }

        private (Vector3, Vector3, Vector3, Vector3) GetBorders(Transform origin, float height, float width, float offset)
        {
            var downLeftPoint = origin.position - origin.right * width / 2f 
                                + _level.OriginPoint.right * offset + Vector3.up * offset;
            
            var downRightPoint = origin.position + origin.right * width / 2f 
                - _level.OriginPoint.right * offset + Vector3.up * offset;

            var upLeftPoint = downLeftPoint + Vector3.up * height;
            var upRightPoint = downRightPoint + Vector3.up * height;
            
            return (upLeftPoint, upRightPoint, downRightPoint, downLeftPoint);
        }

        private bool HasLocatedWithinBorder(Vector3 origin, Vector3 upLeftPoint, Vector3 upRightPoint,
            Vector3 downRightPoint, Vector3 downLeftPoint)
        {
            var rayOrigin = origin - Vector3.forward;

            if (_rayFormulas.IntersectsTriangle(rayOrigin, Vector3.forward,
                    downLeftPoint, upLeftPoint, downRightPoint))
            {
                return true;
            }
            
            if (_rayFormulas.IntersectsTriangle(rayOrigin, Vector3.forward,
                    upLeftPoint, upRightPoint, downRightPoint))
            {
                return true;
            }

            return false;
        }

        private Vector3 GetClosestPointByBorder(Vector3 position, Vector3 upLeftPoint, Vector3 upRightPoint,
            Vector3 downRightPoint, Vector3 downLeftPoint)
        {
            var distance1 = _edgeFormulas.GetClosestPoint(position, upLeftPoint, upRightPoint);
            var distance2 = _edgeFormulas.GetClosestPoint(position, upRightPoint, downRightPoint);
            var distance3 = _edgeFormulas.GetClosestPoint(position, downRightPoint, downLeftPoint);
            var distance4 = _edgeFormulas.GetClosestPoint(position, downLeftPoint, upLeftPoint);
                
            return GetNearest(position, distance1, distance2, distance3, distance4, distance4);
        }

        private Vector3 GetNearest(Vector3 position, params Vector3[] vectors)
        {
            return vectors.OrderBy(vector => Vector3.Distance(vector, position)).First();
        }
    }
}