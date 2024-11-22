using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Providers.Objects.Levels
{
    public interface ILevelsConfigProvider
    {
        UniTask<GameObject> GetLevelPrefabAsync(int index);
        UniTask<GameObject> GetToyPrefabAsync();
    }
}