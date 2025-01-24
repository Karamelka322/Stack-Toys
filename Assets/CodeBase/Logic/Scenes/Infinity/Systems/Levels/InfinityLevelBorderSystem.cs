using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.Interfaces.General.Formulas;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Scenes.Infinity.Systems.Levels
{
    public class InfinityLevelBorderSystem : BaseLevelBorderSystem
    {
        public InfinityLevelBorderSystem(ILevelProvider levelProvider, IRayFormulas rayFormulas,
            IEdgeFormulas edgeFormulas) : base(levelProvider, rayFormulas, edgeFormulas)
        {
            
        }
        
        public override async UniTask<float> GetHeightAsync()
        {
            return 5f;
        }

        public override async UniTask<float> GetWidthAsync()
        {
            return 5.5f;
        }
    }
}