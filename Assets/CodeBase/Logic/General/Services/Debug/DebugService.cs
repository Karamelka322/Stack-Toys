using CodeBase.Data.General.Constants;
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
            if (BuildConstants.Debug)
            {
                SRDebug.Init();
            }
        }

        public void RegisterOptionContainer(object container)
        {
            if (BuildConstants.Debug)
            {
                if (container == null || SRDebug.Instance == null)
                {
                    return;
                }
            
                SRDebug.Instance.AddOptionContainer(container);
            }
        }

        public void UnregisterOptionContainer(object container)
        {
            if (BuildConstants.Debug)
            {
                if (container == null || SRDebug.Instance == null)
                {
                    return;
                }
            
                SRDebug.Instance.RemoveOptionContainer(container);
            }
        }
    }
}