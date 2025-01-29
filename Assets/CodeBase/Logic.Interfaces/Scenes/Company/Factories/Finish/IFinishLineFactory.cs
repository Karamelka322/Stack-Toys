using CodeBase.Logic.Scenes.Company.Systems.Finish;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Factories.Finish
{
    public interface IFinishLineFactory
    {
        UniTask<FinishLine> SpawnAsync(Vector3 position, Quaternion rotation);
    }
}