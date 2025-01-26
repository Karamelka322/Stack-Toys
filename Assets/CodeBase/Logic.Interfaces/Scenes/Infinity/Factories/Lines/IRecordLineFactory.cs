using CodeBase.Logic.Scenes.Infinity.Objects.Lines;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Lines
{
    public interface IRecordLineFactory
    {
        UniTask<RecordLine> SpawnAsync(Vector3 position, string titleLocalizationId, float height);
    }
}