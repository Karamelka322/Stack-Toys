using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Services.Files;
using CodeBase.Logic.Interfaces.General.Services.SaveLoad;
using CodeBase.Logic.Interfaces.General.Services.SaveLoad.Formatters;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Logic.General.Services.SaveLoad
{
    /// <summary>
    /// Для сохранения\загрузки данных
    /// </summary>
    [UsedImplicitly]
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IBinaryFormatter _binaryFormatter;
        private readonly IFileService _fileService;

        public SaveLoadService(IBinaryFormatter binaryFormatter, IFileService fileService)
        {
            _fileService = fileService;
            _binaryFormatter = binaryFormatter;
        }

        public void Save<TData>(TData data) where TData : class
        {
            var obj = _binaryFormatter.Serialize(data);
            _fileService.SaveToFile(obj, data.GetType().Name, DataStorageFormats.Binary);
        }

        public TData Load<TData>() where TData : class, new()
        {
            var obj = _fileService.LoadFile(typeof(TData).Name, DataStorageFormats.Binary);
            
            if (obj is byte[] { Length: 0 })
            {
                return new TData();
            }
            
            return _binaryFormatter.Deserialize<TData>(obj as byte[]);
        }
    }
}