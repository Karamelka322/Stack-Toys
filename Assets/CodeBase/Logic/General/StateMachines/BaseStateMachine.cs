using UniRx;

namespace CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes
{
    /// <summary>
    /// Базовая реализация машины состояний
    /// </summary>
    public abstract class BaseStateMachine
    {
        private CompositeDisposable _compositeDisposable;
        private StateTree _stateTree;
        private BaseState _currentState;

        protected BaseStateMachine()
        {
            _compositeDisposable = new CompositeDisposable();
        }

        /// <summary>
        /// Запустить машину состояний
        /// </summary>
        protected void Launch()
        {
            _stateTree = InstallStateTree();
            ChangeState(_stateTree.GetFirstState());
        }

        /// <summary>
        /// Инициализация дерева состояний
        /// </summary>
        protected abstract StateTree InstallStateTree();
        
        /// <summary>
        /// Войти в состояние
        /// </summary>
        /// <param name="state">Состояние в которое нужно войти</param>
        private void Enter<TState>(TState state) where TState : BaseState
        {
            if (state == null)
            {
                return;
            }
            
            state.Enter();

            foreach (var tuple in _stateTree.GetTransitions(state))
            {
                var nextState = tuple.Item3;

                tuple.Item2.Enter();

                if (tuple.Item2.IsCompleted.Value)
                {
                    ChangeState(nextState);
                    return;
                }

                tuple.Item2.IsCompleted.Subscribe(
                    isCompleted => OnTransitionTrigger(isCompleted, nextState)).AddTo(_compositeDisposable);
            }
        }

        /// <summary>
        /// Выйти из состояния
        /// </summary>
        /// <param name="state">Состояние из которого нужно выйти</param>
        private void Exit<TState>(TState state) where TState : BaseState
        {
            if (state == null)
            {
                return;
            }
            
            state.Exit();

            foreach (var tuple in _stateTree.GetTransitions(state))
            {
                tuple.Item2.Exit();
            }
            
            _compositeDisposable?.Dispose();
            _compositeDisposable = new CompositeDisposable();
        }
        
        /// <summary>
        /// Поменять текущее активное состояние на другое
        /// </summary>
        /// <param name="state">Состояние на которое меняем текущее</param>
        private void ChangeState<TState>(TState state) where TState : BaseState
        {
            if (_currentState != null)
            {
                Exit(_currentState);
            }
            
            _currentState = state;   
            Enter(_currentState);
        }

        /// <summary>
        /// Если отрабатывает один из переходов, то переходим в следующее состояние
        /// </summary>
        /// <param name="isCompleted"></param>
        /// <param name="nextState">Следующее состояние</param>
        private void OnTransitionTrigger(bool isCompleted, BaseState nextState)
        {
            if (isCompleted == false)
            {
                return;
            }
            
            ChangeState(nextState);
        }
    }
}