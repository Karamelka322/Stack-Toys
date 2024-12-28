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
            if (container == null || SRDebug.Instance == null)
            {
                return;
            }
            
#if !DISABLE_SRDEBUGGER
            SRDebug.Instance.AddOptionContainer(container);
#endif
            
        }

        public void UnregisterOptionContainer(object container)
        {
            if (container == null || SRDebug.Instance == null)
            {
                return;
            }
            
#if !DISABLE_SRDEBUGGER
            SRDebug.Instance.RemoveOptionContainer(container);
#endif
            
        }
    }
}