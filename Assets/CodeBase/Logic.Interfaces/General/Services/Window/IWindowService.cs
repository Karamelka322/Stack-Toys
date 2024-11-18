using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Services.Window
{
    public interface IWindowService
    {
        UniTask<Canvas> GetCanvasAsync();
    }
}