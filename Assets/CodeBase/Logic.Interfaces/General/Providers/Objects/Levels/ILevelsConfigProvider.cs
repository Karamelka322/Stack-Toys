using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Data.ScriptableObjects.Levels
{
    public interface ILevelsConfigProvider
    {
        UniTask<GameObject> GetLevelPrefabAsync();
    }
}