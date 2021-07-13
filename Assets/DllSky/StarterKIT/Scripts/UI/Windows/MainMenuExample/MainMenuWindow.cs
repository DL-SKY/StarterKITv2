﻿using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Constants;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.UI.Windows.Loading;
using DllSky.StarterKITv2.UI.Windows.SecondExampleWindow;
using UnityEngine;
using UnityEngine.UI;

namespace DllSky.StarterKITv2.UI.Windows.MainMenuExample
{
    public class MainMenuWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\MainMenu\MainMenuWindow";

        [Space()]
        [SerializeField] private Text _label;

        private GameManager _gameManager;
        private IWindowsManagerUsing _windowsManagerHolder;
        private IWindowSceneLoader _loadingWindow;


        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _windowsManagerHolder = GameManager.Instance;
        }


        public override void Initialize(object data)
        {
            _label.text = ((int)data).ToString();

            if (_gameManager.CheckCurrentScene(ConstantScenes.EXAMPLE_MAIN_MENU))
            {
                SetInitialize(true);
            }
            else
            {
                _loadingWindow = _windowsManagerHolder.WindowsController.CreateWindow<SceneLoadingWindow>(SceneLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading, ConstantScenes.EXAMPLE_MAIN_MENU);
                _loadingWindow.OnSceneLoaded += OnSceneLoadedHandler;
            }
        }

        public void OnClick()
        {
            _windowsManagerHolder.WindowsController.CreateWindow<SecondWindow>(SecondWindow.prefabPath, Enums.EnumWindowsLayer.Main);

            Close();
        }


        private void OnSceneLoadedHandler()
        {
            if (_loadingWindow != null)
            {
                _loadingWindow.OnSceneLoaded -= OnSceneLoadedHandler;
                _loadingWindow.CloseLoaderWindow();
            }

            SetInitialize(true);
        }
    }
}
