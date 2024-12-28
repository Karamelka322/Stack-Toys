namespace CodeBase.Logic.Interfaces.General.Services.Debug
{
    public interface IDebugService
    {
        void RegisterOptionContainer(object container);
        void UnregisterOptionContainer(object container);
    }
}