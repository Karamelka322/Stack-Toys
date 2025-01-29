using CodeBase.Logic.General.Systems.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Factories.Finish
{
    public abstract class BaseLineFactory
    {
        private const float _heightLabelOffset = 0.1f;
        
        private readonly ILevelSizeSystem _levelSizeSystem;

        public BaseLineFactory(ILevelSizeSystem levelSizeSystem)
        {
            _levelSizeSystem = levelSizeSystem;
        }
        
        protected async UniTask ScaleAsync(RectTransform height, SpriteRenderer line)
        {
            var width = await _levelSizeSystem.GetWidthAsync();
            
            height.anchoredPosition3D = new Vector3()
            {
                x = (width / 2f) - _heightLabelOffset,
                y = height.anchoredPosition3D.y,
                z = height.anchoredPosition3D.z
            };

            line.size = new Vector2(width, line.size.y);
        }
    }
}