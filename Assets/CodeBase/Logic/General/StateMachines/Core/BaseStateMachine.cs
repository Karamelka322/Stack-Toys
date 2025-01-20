using System;
using System.Collections.Generic;
using UniRx;

namespace CodeBase.Logic.General.StateMachines.Core
{
    /// <summary>
    /// Базовая реализация машины состояний
    /// </summary>
    public abstract class BaseStateMachine
    {
        private CompositeDisposable _compositeDisposable;
        private StateTree _stateTree;
        private BaseState _currentState;

        private readonly List<(BaseState, Action)> _enterStateListeners;
        private readonly List<(BaseState, Action)> _exitStateListeners;
        
        protected BaseStateMachine()
        {
            _enterStateListeners = new List<(BaseState, Action)>();
            _exitStateListeners = new List<(BaseState, Action)>();
            _compositeDisposable = new CompositeDisposable();
        }

        /// <summary>
        /// Запустить машину состояний
        /// </summary>
        public void Launch()
        {
            _stateTree = InstallStateTree();
            ChangeState(_stateTree.GetFirstState());
        }

        public void Reset()
        {
            _currentState?.Exit();

            var transitions = _stateTree.GetTransitions(_currentState);

            foreach (var transition in transitions)
            {
                transition.Item2.Exit();
            }
        }

        /// <summary>
        /// Инициализация дерева состояний
        /// </summary>
        protected abstract StateTree InstallStateTree();
        
        public void SubscribeToEnterState<TState>(Action callback) where TState : BaseState
        {
            var state = _stateTree.GetState(typeof(TState));
            _enterStateListeners.Add((state, callback));
        }
        
        public void SubscribeToExitState<TState>(Action callback) where TState : BaseState
        {
            var state = _stateTree.GetState(typeof(TState));
            _exitStateListeners.Add((state, callback));
        }
        
        /// <summary>
        /// Войти в состояние
        /// </summary>
        /// <param name="state">Состояние в которое нужно войти</param>
        private void Enter<TState>(TState state) where TState : BaseState
        {
            state?.Enter();
            NotifyEnterStateListeners(state);

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
            state?.Exit();
            NotifyExitStateListeners(state);

            _compositeDisposable?.Dispose();
            _compositeDisposable = new CompositeDisposable();
            
            foreach (var tuple in _stateTree.GetTransitions(state))
            {
                tuple.Item2.Exit();
                tuple.Item2.IsCompleted.Value = false;
            }
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
        
        private void NotifyEnterStateListeners<TState>(TState state) where TState : BaseState
        {
            foreach (var listener in _enterStateListeners)
            {
                if (listener.Item1 == state)
                {
                    listener.Item2?.Invoke();
                }
            }
        }
        
        private void NotifyExitStateListeners<TState>(TState state) where TState : BaseState
        {
            foreach (var listener in _exitStateListeners)
            {
                if (listener.Item1 == state)
                {
                    listener.Item2?.Invoke();
                }
            }
        }
    }
}