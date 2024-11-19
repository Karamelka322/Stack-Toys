using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Services.Window
{
    public interface IWindowCanvasProvider
    {
        UniTask<Canvas> GetCanvasAsync();
    }
}