using CodeBase.Logic.Scenes.Infinity.Objects.Lines;
using UniRx;

namespace CodeBase.Logic.Scenes.Infinity.Providers.Lines
{
    public class RecordLineProvider : IRecordLineProvider
    {
        public ReactiveProperty<RecordLine> WorldRecordLine { get; }
        public ReactiveProperty<RecordLine> PlayerRecordLine { get; }

        public RecordLineProvider()
        {
            WorldRecordLine = new ReactiveProperty<RecordLine>();
            PlayerRecordLine = new ReactiveProperty<RecordLine>();
        }
    }
}