using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Factories.Confetti
{
    public interface IConfettiEffectFactory
    {
        UniTask<GameObject> SpawnAsync(Vector3 position, Quaternion rotation);
    }
}