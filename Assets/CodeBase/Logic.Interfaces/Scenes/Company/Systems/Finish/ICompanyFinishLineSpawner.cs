using System;
using CodeBase.Logic.General.Unity.Finish;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish
{
    public interface ICompanyFinishLineSpawner
    {
        event Action<FinishLineMediator> OnSpawn;
    }
}