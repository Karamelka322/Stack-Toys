using System;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems
{
    public class CompanySceneSaver : IDisposable
    {
        private readonly IPlayerSaveDataProvider _playerSaveDataProvider;

        public CompanySceneSaver(IPlayerSaveDataProvider playerSaveDataProvider)
        {
            _playerSaveDataProvider = playerSaveDataProvider;
            
            Application.focusChanged += OnFocusChanged;
            Application.quitting += OnClosing;
        }

        public void Dispose()
        {
            Application.quitting -= OnClosing;
            Application.focusChanged -= OnFocusChanged;
        }

        private void OnFocusChanged(bool isFocused)
        {
            if (isFocused == false)
            {
                _playerSaveDataProvider.Save();
            }
        }

        private void OnClosing()
        {
            _playerSaveDataProvider.Save();
        }
    }
}