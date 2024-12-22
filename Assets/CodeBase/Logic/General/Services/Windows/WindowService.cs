using System.Collections.Generic;
using CodeBase.Logic.Interfaces.General.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.General.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly List<BaseWindow> _windows;
        private readonly Stack<BaseWindow> _stack;
        
        public WindowService()
        {
            _stack = new Stack<BaseWindow>();
            _windows = new List<BaseWindow>();
            
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        
        ~WindowService()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        public void RegisterWindow<TWindow>(TWindow window) where TWindow : BaseWindow
        {
            _windows.Add(window);
        }

        public async UniTask OpenAsync<TWindow>() where TWindow : BaseWindow
        {
            await TryHideAsyncCurrentWindow();

            var window = GetWindow<TWindow>();

            window.Open();
            await window.OpenAsync();

            _stack.Push(window);
        }

        public async UniTask CloseAsync<TWindow>() where TWindow : BaseWindow
        {
            if (_stack.TryPeek(out var currentWindow) == false || currentWindow is TWindow == false)
            {
                UnityEngine.Debug.LogError($"Window {typeof(TWindow).Name} is not current");
                return;
            }
            
            var window = GetWindow<TWindow>();

            window.Close();
            await window.CloseAsync();

            _stack.Pop();

            await TryShowAsyncCurrentWindow();
        }

        private async UniTask TryHideAsyncCurrentWindow()
        {
            if (_stack.TryPeek(out var currentWindow))
            {
                currentWindow.Hide();
                await currentWindow.HideAsync();
            }
        }
        
        private async UniTask TryShowAsyncCurrentWindow()
        {
            if (_stack.TryPeek(out var currentWindow))
            {
                currentWindow.Show();
                await currentWindow.ShowAsync();
            }
        }

        private BaseWindow GetWindow<TWindow>() where TWindow : BaseWindow
        {
            foreach (var window in _windows)
            {
                if (window is TWindow)
                {
                    return window;
                }
            }

            return null;
        }

        private void OnSceneUnloaded(Scene scene)
        {
            _windows.Clear();
        }
    }
}