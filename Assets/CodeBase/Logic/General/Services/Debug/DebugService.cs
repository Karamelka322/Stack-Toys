using CodeBase.Logic.Interfaces.General.Services.Debug;
using JetBrains.Annotations;

namespace CodeBase.Logic.General.Services.Debug
{
    /// <summary>
    /// Сервис для отладки игры
    /// </summary>
    [UsedImplicitly]
    public class DebugService : IDebugService
    {
        public DebugService()
        {
            
#if !DISABLE_SRDEBUGGER
            SRDebug.Init();
#endif

        }

        public void RegisterOptionContainer(object container)
        {
            
#if !DISABLE_SRDEBUGGER

            if (container == null || SRDebug.Instance == null)
            {
                return;
            }
            
            SRDebug.Instance.AddOptionContainer(container);
#endif
            
        }

        public void UnregisterOptionContainer(object container)
        {
            
#if !DISABLE_SRDEBUGGER

            if (container == null || SRDebug.Instance == null)
            {
                return;
            }
            
            SRDebug.Instance.RemoveOptionContainer(container);
#endif
            
        }
    }
}