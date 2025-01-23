using System;
using UnityEngine;

namespace CodeBase.Logic.General.Observers.Toys
{
    public interface IToyCollisionObserver
    {
        event Action<GameObject> OnCollision;
    }
}