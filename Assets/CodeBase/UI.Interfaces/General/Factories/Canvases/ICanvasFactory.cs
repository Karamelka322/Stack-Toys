using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Interfaces.General.Factories.Canvases
{
    public interface ICanvasFactory
    {
        UniTask<Canvas> SpawnAsync(string name);
    }
}