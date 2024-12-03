using CodeBase.Logic.Interfaces.General.Services.Windows;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.General.Services.Windows
{
    public abstract class BaseWindow
    {
        protected BaseWindow(IWindowService windowService)
        {
            windowService.RegisterWindow(this);
        }

        public virtual void Open() { }
        public virtual UniTask OpenAsync() => UniTask.CompletedTask;

        public virtual void Close() { }
        public virtual UniTask CloseAsync() => UniTask.CompletedTask;
        
        public virtual void Show() { }
        public virtual UniTask ShowAsync() => UniTask.CompletedTask;
        
        public virtual void Hide() { }
        public virtual UniTask HideAsync() => UniTask.CompletedTask;
    }
}