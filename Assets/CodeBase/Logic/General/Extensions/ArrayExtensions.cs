namespace CodeBase.Logic.General.Extensions
{
    public static class ArrayExtensions
    {
        public static TValue Random<TValue>(this TValue[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
    }
}