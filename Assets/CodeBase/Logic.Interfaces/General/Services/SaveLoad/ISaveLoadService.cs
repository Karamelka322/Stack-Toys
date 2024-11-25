namespace CodeBase.Logic.Interfaces.General.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void Save<TData>(TData data) where TData : class;
        TData Load<TData>() where TData : class, new();
    }
}