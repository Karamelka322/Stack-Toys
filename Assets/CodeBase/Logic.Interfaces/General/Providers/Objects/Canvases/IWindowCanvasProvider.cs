using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Providers.Objects.Canvases
{
    public interface IWindowCanvasProvider
    {
        UniTask<Canvas> GetCanvasAsync();
    }
}