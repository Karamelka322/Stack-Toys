using System;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Observers.Toys
{
    public interface IToyCollisionObserver
    {
        event Action<GameObject> OnCollision;
    }
}