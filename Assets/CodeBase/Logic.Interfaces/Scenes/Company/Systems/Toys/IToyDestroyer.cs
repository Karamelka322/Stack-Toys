using System;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys
{
    public interface IToyDestroyer
    {
        event Action OnDestroyAll;
    }
}