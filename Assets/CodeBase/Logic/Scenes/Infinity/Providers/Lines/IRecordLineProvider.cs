using CodeBase.Logic.Scenes.Infinity.Objects.Lines;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Providers.Lines
{
    public interface IRecordLineProvider
    {
        ReactiveProperty<RecordLine> WorldRecordLine { get; }
        ReactiveProperty<RecordLine> PlayerRecordLine { get; }
    }
}