using UnityEngine;

namespace CodeBase.Logic.General.Commands
{
    public interface IRaycastCommand
    {
        bool HasSelect(Vector3 clickPosition, GameObject target);
        bool HasUI(Vector3 clickPosition);
    }
}