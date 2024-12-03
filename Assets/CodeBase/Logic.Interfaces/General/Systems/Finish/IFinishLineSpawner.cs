using System;
using CodeBase.Logic.General.Unity.Finish;

namespace CodeBase.Logic.Interfaces.General.Systems.Finish
{
    public interface IFinishLineSpawner
    {
        event Action<FinishLineMediator> OnSpawn;
    }
}