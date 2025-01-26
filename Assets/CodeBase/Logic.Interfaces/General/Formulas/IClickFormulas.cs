using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Formulas
{
    public interface IClickFormulas
    {
        bool HasSelect(Vector3 clickPosition, GameObject target);
        bool HasUI(Vector3 clickPosition);
        Vector3 ClickToWorldPosition(Camera camera, Vector3 clickPosition, Transform target);
    }
}