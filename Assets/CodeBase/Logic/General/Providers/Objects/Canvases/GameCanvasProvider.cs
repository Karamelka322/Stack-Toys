using CodeBase.Logic.Interfaces.General.Providers.Objects.Canvases;
using CodeBase.UI.Interfaces.General.Factories.Canvases;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Providers.Objects.Canvases
{
    public class GameCanvasProvider : IGameCanvasProvider
    {
        private readonly ICanvasFactory _canvasFactory;

        private Canvas _canvas;

        public GameCanvasProvider(ICanvasFactory canvasFactory)
        {
            _canvasFactory = canvasFactory;
        }

        public async UniTask<Canvas> GetCanvasAsync()
        {
            if (_canvas == null)
            {
                _canvas = await _canvasFactory.SpawnAsync("UI - Game");
            }

            return _canvas;
        }
    }
}