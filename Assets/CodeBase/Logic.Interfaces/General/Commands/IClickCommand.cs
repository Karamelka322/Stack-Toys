using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Commands
{
    public interface IClickCommand
    {
        bool HasSelect(Vector3 clickPosition, GameObject target);
        bool HasUI(Vector3 clickPosition);
    }
}