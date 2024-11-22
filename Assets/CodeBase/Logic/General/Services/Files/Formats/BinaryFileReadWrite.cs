using System.IO;

namespace CodeBase.Logic.General.Services.Files.Formats
{
    /// <summary>
    /// Утилита для работы с бинарными файлами
    /// </summary>
    public class BinaryFileReadWrite
    {
        public void WriteBinaryFile(byte[] bytes, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fileStream.Write(bytes);
            }
        }

        public byte[] ReadBinaryFile(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);

                return bytes;
            }
        }
    }
}