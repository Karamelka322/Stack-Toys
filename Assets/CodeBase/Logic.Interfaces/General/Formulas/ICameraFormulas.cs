using UnityEngine;

namespace CodeBase.Logic.General.Formulas
{
    public interface ICameraFormulas
    {
        bool HasLocatedWithinCameraFieldOfView(Vector3 position);
    }
}