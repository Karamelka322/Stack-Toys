using CodeBase.Logic.General.Unity.Finish;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Factories.Finish
{
    public interface IFinishLineFactory
    {
        UniTask<FinishLineMediator> SpawnAsync(Vector3 position, Quaternion rotation);
    }
}