using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Services.Window
{
    public interface IGameCanvasProvider
    {
        UniTask<Canvas> GetCanvasAsync();
    }
}