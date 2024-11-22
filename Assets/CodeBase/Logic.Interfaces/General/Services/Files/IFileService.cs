namespace CodeBase.Logic.Interfaces.General.Services.Files
{
    public interface IFileService
    {
        void SaveToFile(object obj, string fileName, string dataFormat);
        object LoadFile(string fileName, string dataFormat);
    }
}