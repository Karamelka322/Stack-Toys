using System;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Services.Input
{
    public interface IInputService
    {
        event Action<Vector3> OnClickDown;
        event Action<Vector3> OnClick;
        event Action<Vector3> OnClickUp;
        event Action<Vector3> OnSwipe;
        bool IsClickPressed { get; }
    }
}