using System;
using CodeBase.Logic.General.StateMachines.Toys;
using CodeBase.Logic.General.Unity.Toys;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys
{
    public interface IToySpawner
    {
        event Action<ToyMediator, ToyStateMachine> OnSpawn;
    }
}