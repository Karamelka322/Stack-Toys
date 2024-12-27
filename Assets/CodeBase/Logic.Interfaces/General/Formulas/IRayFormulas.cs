using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Levels
{
    public interface IRayFormulas
    {
        /// <summary>
        /// Функция для проверки пересечения луча с треугольником
        /// </summary>
        /// <param name="rayOrigin">Начало луча</param>
        /// <param name="rayDirection">Направления луча</param>
        /// <param name="v0">Первая точка полигона</param>
        /// <param name="v1">Вторая точка полигона</param>
        /// <param name="v2">Третья точка полигона</param>
        bool IntersectsTriangle(Vector3 rayOrigin, Vector3 rayDirection, Vector3 v0, Vector3 v1, Vector3 v2);
    }
}