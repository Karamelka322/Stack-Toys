using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.General.Factories.Toys
{
    public interface IToyFactory
    {
        UniTask<ToyMediator> SpawnAsync(Vector3 position);
        event Action<ToyMediator, ToyStateMachine> OnSpawn;
    }
}