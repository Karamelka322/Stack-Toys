namespace CodeBase.Logic.Interfaces.General.Services.SaveLoad.Formatters
{
    public interface IBinaryFormatter
    {
        byte[] Serialize<TData>(TData data);
        TData Deserialize<TData>(byte[] bytes);
    }
}