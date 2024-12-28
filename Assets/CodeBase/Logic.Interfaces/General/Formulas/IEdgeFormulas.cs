using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Formulas
{
    public interface IEdgeFormulas
    {
        /// <summary>
        /// Функция для нахождения ближайшей точки на отрезке
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="edgePointA">Начало отрезка</param>
        /// <param name="edgePointB">Конец отрезка</param>
        /// <returns></returns>
        Vector3 GetClosestPoint(Vector3 point, Vector3 edgePointA, Vector3 edgePointB);
    }
}