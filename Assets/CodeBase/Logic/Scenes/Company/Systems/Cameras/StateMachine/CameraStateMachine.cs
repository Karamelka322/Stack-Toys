using System;
using CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes;
using CodeBase.Logic.Scenes.Company.Systems.Cameras.States;
using CodeBase.Logic.Scenes.Company.Systems.Cameras.Transitions;
using CodeBase.Logic.Scenes.Company.Systems.Load;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras
{
    public class CameraStateMachine : BaseStateMachine
    {
        private readonly IDisposable _disposable;
        private readonly Camera _camera;

        private readonly CameraScrollState.Factory _scrollStateFactory;
        private readonly CameraToyFollowState.Factory _toyFollowStateFactory;
        
        private readonly CameraToySelectTransition.Factory _toySelectTransitionFactory;

        public CameraStateMachine(
            ICompanySceneLoad sceneLoad, 
            CameraScrollState.Factory scrollStateFactory,
            CameraToyFollowState.Factory toyFollowStateFactory,
            CameraToySelectTransition.Factory toySelectTransitionFactory)
        {
            _toyFollowStateFactory = toyFollowStateFactory;
            _toySelectTransitionFactory = toySelectTransitionFactory;
            _camera = Camera.main;
            _scrollStateFactory = scrollStateFactory;
            
            _disposable = sceneLoad.IsLoaded.Subscribe(OnSceneLoaded);
        }

        private void OnSceneLoaded(bool isLoaded)
        {
            if (isLoaded == false)
            {
                return;
            }
            
            Launch();
            
            _disposable?.Dispose();
        }

        protected override StateTree InstallStateTree()
        {
            var scrollState = _scrollStateFactory.Create(_camera);
            var toyFollowState = _toyFollowStateFactory.Create(_camera);
            
            var toySelectTransition = _toySelectTransitionFactory.Create();
            
            var tree = new StateTree();
            
            tree.RegisterState(scrollState);
            tree.RegisterTransition(scrollState, toySelectTransition, toyFollowState);

            tree.RegisterState(toyFollowState);
            
            return tree;
        }
    }
}