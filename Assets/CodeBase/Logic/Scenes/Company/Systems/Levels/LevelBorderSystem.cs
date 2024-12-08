using System;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Scenes.Company.Unity;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Levels
{
    public class LevelBorderSystem : ILevelBorderSystem, IDisposable
    {
        private const float TopBorder = 4f;

        private readonly ICompanyLevelsSettingProvider _companyLevelsSettingProvider;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly IDisposable _disposable;

        private LevelMediator _level;

        public Vector3 OriginPoint { get; private set; }
        public Vector3 BottomLeftPoint { get; private set; }
        public Vector3 BottomRightPoint { get; private set; }
        public Vector3 TopLeftPoint { get; private set; }
        public Vector3 TopRightPoint { get; private set; }
        
        public LevelBorderSystem(
            ILevelProvider levelProvider, 
            ICompanyLevelsSettingProvider companyLevelsSettingProvider, 
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider)
        {
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _companyLevelsSettingProvider = companyLevelsSettingProvider;
            _disposable = levelProvider.Level.Subscribe(OnLevelLoaded);
        }

        private async void OnLevelLoaded(LevelMediator levelMediator)
        {
            if (levelMediator == null)
            {
                return;
            }
        
            _level = levelMediator;

            OriginPoint = _level.OriginPoint.position;
            
            BottomLeftPoint = _level.OriginPoint.position - _level.OriginPoint.right * _level.Width / 2f;
            BottomRightPoint = _level.OriginPoint.position + _level.OriginPoint.right * _level.Width / 2f;
        
            TopLeftPoint = BottomLeftPoint + _level.OriginPoint.up * await GetHeightAsync();
            TopRightPoint = BottomRightPoint + _level.OriginPoint.up * await GetHeightAsync();
        }

        public async UniTask<float> GetHeightAsync()
        {
            var currentLevel = _companyLevelsSaveDataProvider.GetCurrentLevel();
            return await _companyLevelsSettingProvider.GetLevelHeightAsync(currentLevel);
        }

        public async UniTask<Vector3> ClampAsync(ToyMediator toyMediator, Vector3 position)
        {
            var clampPosition = position;
            var size = toyMediator.MeshRenderer.bounds.size;
            var max = Mathf.Max(size.x, size.y) / 2f;

            var levelHeight = await GetHeightAsync();

            clampPosition.x = Mathf.Clamp(clampPosition.x, BottomLeftPoint.x + max, BottomRightPoint.x - max);
            clampPosition.y = Mathf.Clamp(clampPosition.y, 
                _level.OriginPoint.position.y + max, (_level.OriginPoint.up * levelHeight).y - max + TopBorder);
            
            return clampPosition;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}