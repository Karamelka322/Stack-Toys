namespace CodeBase.Logic.General.StateMachines.Core
{
    /// <summary>
    /// Базовая реализация состояния
    /// </summary>
    public abstract class BaseState
    {
        /// <summary>
        /// При входе в состояние
        /// </summary>
        public virtual void Enter() { }
        
        /// <summary>
        /// При выходе из состояния
        /// </summary>
        public virtual void Exit() { }
    }
}