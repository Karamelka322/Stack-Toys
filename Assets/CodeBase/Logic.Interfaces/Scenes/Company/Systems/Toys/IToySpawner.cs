using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys
{
    public interface IToySpawner
    {
        event Action<ToyMediator, ToyStateMachine> OnSpawn;
    }
}