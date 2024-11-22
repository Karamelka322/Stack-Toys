using System;
using CodeBase.Data.Constants;
using CodeBase.Logic.General.Services.Files.Formats;
using CodeBase.Logic.Interfaces.General.Services.Files;
using JetBrains.Annotations;

namespace CodeBase.Logic.General.Services.Files
{
    /// <summary>
    /// Для работы с файлами
    /// </summary>
    [UsedImplicitly]
    public class FileService : IFileService
    {
        private readonly BinaryFileReadWrite _binaryFileReadWrite;

        public FileService()
        {
            _binaryFileReadWrite = new BinaryFileReadWrite();
        }

        public void SaveToFile(object obj, string fileName, string dataFormat)
        {
            var filePath = GetFilePath(fileName, dataFormat);

            if (obj is byte[] bytes)
            {
                _binaryFileReadWrite.WriteBinaryFile(bytes, filePath);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Type not to be saved {obj.GetType().Name}. File name {fileName}");
            }
        }

        public object LoadFile(string fileName, string dataFormat)
        {
            var filePath = GetFilePath(fileName, dataFormat);

            if (dataFormat == DataStorageFormats.Binary)
            {
                return _binaryFileReadWrite.ReadBinaryFile(filePath);
            }
            
            throw new ArgumentOutOfRangeException($"Load file error {fileName}, data format {dataFormat}");
        }

        private static string GetFilePath(string fileName, string dataFormat)
        {
            return $"{UnityEngine.Application.persistentDataPath}/{fileName}.{dataFormat}";
        }
    }
}