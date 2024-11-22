using System;
using System.IO;
using CodeBase.Logic.Interfaces.General.Services.SaveLoad.Formatters;
using JetBrains.Annotations;

namespace CodeBase.Logic.General.Services.SaveLoad.Formatters
{
    using Formatter = System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;

    /// <summary>
    /// Средство для форматирования данных с целью сохранения\загрузки в бинарном формате
    /// </summary>
    [UsedImplicitly]
    public class BinaryFormatter : IBinaryFormatter
    {
        private readonly Formatter _binaryFormatter;

        public BinaryFormatter()
        {
            _binaryFormatter = new Formatter();
        }
        
        public byte[] Serialize<TData>(TData data)
        {
            using var memoryStream = new MemoryStream();

            if (Attribute.IsDefined(data.GetType(), typeof(SerializableAttribute)) == false)
            {
                throw new ArithmeticException($"Type {data.GetType().Name} is not serializable");
            }
            
            _binaryFormatter.Serialize(memoryStream, data);
            
            return memoryStream.ToArray();
        }

        public TData Deserialize<TData>(byte[] bytes)
        {
            using var memoryStream = new MemoryStream();

            if (bytes.Length == 0)
            {
                return default;
            }

            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
                
            return (TData)_binaryFormatter.Deserialize(memoryStream);
        }
    }
}