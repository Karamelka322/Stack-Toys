using System;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.Logic.Scenes.Company.Systems.Load;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras
{
    public class CameraRenderSetup : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly Camera _camera;

        public CameraRenderSetup(ICompanyLevelSpawner sceneLoad)
        {
            _camera = Camera.main;
            
            _disposable = sceneLoad.IsLoaded.Subscribe(OnSceneLoaded);
        }
        
        private void OnSceneLoaded(bool isLoaded)
        {
            if (isLoaded == false)
            {
                return;
            }

            _camera.UpdateVolumeStack();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}