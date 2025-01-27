using System.Collections.Generic;

namespace CodeBase.Logic.General.Extensions
{
    public static class ArrayExtensions
    {
        public static TValue Random<TValue>(this TValue[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
        
        public static TValue Random<TValue>(this TValue[] array, List<TValue> exclude)
        {
            if (exclude.Count == 0)
            {
                return Random(array);
            }
            
            var list = new List<TValue>(array.Length);

            for (int i = 0; i < array.Length; i++)
            {
                if (exclude.Contains(array[i]) == false)
                {
                    list.Add(array[i]);
                }
            }
            
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}