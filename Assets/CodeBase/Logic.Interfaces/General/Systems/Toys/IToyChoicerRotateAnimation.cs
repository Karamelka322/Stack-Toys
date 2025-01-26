using CodeBase.Logic.Scenes.Infinity.Unity.Toys;

namespace CodeBase.Logic.Interfaces.General.Systems.Toys
{
    public interface IToyChoicerRotateAnimation
    {
        void Play(ToyChoicerMediator toyChoicerMediator);
        void Stop(ToyChoicerMediator toyChoicerMediator);
    }
}