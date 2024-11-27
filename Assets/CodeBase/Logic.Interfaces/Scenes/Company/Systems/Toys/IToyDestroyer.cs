using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public interface IToyDestroyer
    {
        event Action OnDestroyAll;
    }
}