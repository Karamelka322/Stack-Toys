using System;
using UnityEngine;

namespace CodeBase.Logic.General.Services.Input
{
    public interface IInputService
    {
        event Action<Vector3> OnClickDown;
        event Action<Vector3> OnClick;
        event Action<Vector3> OnClickUp;
    }
}