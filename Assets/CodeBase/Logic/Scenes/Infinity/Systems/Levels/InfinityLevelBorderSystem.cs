using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.Interfaces.General.Formulas;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Records;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Scenes.Infinity.Systems.Levels
{
    public class InfinityLevelBorderSystem : BaseLevelBorderSystem
    {
        private readonly IInfinityRecordSystem _infinityRecordSystem;

        public InfinityLevelBorderSystem(
            ILevelProvider levelProvider,
            IRayFormulas rayFormulas,
            IEdgeFormulas edgeFormulas,
            IInfinityRecordSystem infinityRecordSystem) : base(levelProvider, rayFormulas, edgeFormulas)
        {
            _infinityRecordSystem = infinityRecordSystem;
        }
        
        public override async UniTask<float> GetHeightAsync()
        {
            return _infinityRecordSystem.WorldRecord.Value;
        }

        public override async UniTask<float> GetWidthAsync()
        {
            return 5.5f;
        }
    }
}