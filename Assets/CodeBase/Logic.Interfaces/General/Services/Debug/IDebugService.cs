namespace CodeBase.CodeBase.Logic.Services.Debug
{
    public interface IDebugService
    {
        void RegisterOptionContainer(object container);
        void UnregisterOptionContainer(object container);
    }
}