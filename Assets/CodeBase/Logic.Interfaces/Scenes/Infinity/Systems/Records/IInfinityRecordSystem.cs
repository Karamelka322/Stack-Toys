using UniRx;

namespace CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Records
{
    public interface IInfinityRecordSystem
    {
        FloatReactiveProperty PlayerRecord { get; }
        FloatReactiveProperty WorldRecord { get; }
        BoolReactiveProperty IsReady { get; }
    }
}