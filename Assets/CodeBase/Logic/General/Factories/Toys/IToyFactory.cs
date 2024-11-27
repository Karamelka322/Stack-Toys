using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using UnityEngine;

namespace CodeBase.Logic.General.Factories.Toys
{
    public interface IToyFactory
    {
        (ToyMediator, ToyStateMachine) Spawn(GameObject prefab, Vector3 position);
    }
}