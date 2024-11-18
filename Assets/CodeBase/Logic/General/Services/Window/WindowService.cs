using CodeBase.Logic.Interfaces.Services.Window;
using CodeBase.UI.Interfaces.General.Factories.Canvases;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly ICanvasFactory _canvasFactory;

        private Canvas _canvas;

        public WindowService(ICanvasFactory canvasFactory)
        {
            _canvasFactory = canvasFactory;
        }

        public async UniTask<Canvas> GetCanvasAsync()
        {
            if (_canvas == null)
            {
                _canvas = await _canvasFactory.SpawnAsync("UI - Windows");
            }

            return _canvas;
        }
    }
}