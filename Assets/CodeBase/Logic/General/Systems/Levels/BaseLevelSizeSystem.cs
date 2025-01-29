using CodeBase.Logic.General.Formulas;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Systems.Levels
{
    public abstract class BaseLevelSizeSystem : ILevelSizeSystem
    {
        private const float _step = 0.15f;
        
        private readonly ICameraFormulas _cameraFormulas;

        public FloatReactiveProperty Height { get; }
        public FloatReactiveProperty Width { get; }

        public BaseLevelSizeSystem(ICameraFormulas cameraFormulas)
        {
            _cameraFormulas = cameraFormulas;

            Height = new FloatReactiveProperty();
            Width = new FloatReactiveProperty();
        }

        public abstract UniTask<float> GetHeightAsync();
        public abstract UniTask<float> GetWidthAsync();

        protected float CalculateWidth(Transform originPoint)
        {
            var offset = Vector3.zero;
            var origin = originPoint.position;
            var step = originPoint.right * _step;
            
            for (int i = 0; i < 100; i++)
            {
                var isLocated = _cameraFormulas.HasLocatedWithinCameraFieldOfView(origin + offset);

                if (isLocated)
                {
                    offset += step;
                }
                else
                {
                    return Vector3.Distance(origin, origin + offset - step) * 2;
                }
            }
            
            Debug.Log("Not calculated level width");
            
            return 0f;
        }
    }
}