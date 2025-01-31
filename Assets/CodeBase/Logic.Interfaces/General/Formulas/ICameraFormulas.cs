using UnityEngine;

namespace CodeBase.Logic.General.Formulas
{
    public interface ICameraFormulas
    {
        /// <summary>
        /// Находиться ли точка в области камеры
        /// </summary>
        /// <param name="position">Точка</param>
        bool HasLocatedWithinCameraFieldOfView(Vector3 position);

        /// <summary>
        /// Находиться ли точка в области ширины камеры
        /// </summary>
        /// <param name="position">Точка</param>
        bool HasLocatedWithinCameraWidth(Vector3 position);
    }
}