using CodeBase.Logic.General.Unity.Toys;

namespace CodeBase.Logic.Interfaces.General.Systems.Toys
{
    public interface IToyRotateAnimation
    {
        void Play(ToyMediator toyMediator);
        void Stop(ToyMediator toyMediator);
    }
}