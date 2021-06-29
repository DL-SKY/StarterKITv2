using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Constants;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.UI.Windows.MainMenuExample;
using System;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows.Loading
{
    public class GameLoadingWindow : WindowBase, IWindowSceneLoader
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Loading\GameLoadingWindow";

        public event Action OnSceneLoaded;

        public string SceneName { get; private set; }
        public AsyncOperation LoadingProc { get; private set; }

        private IWindowInitializer _window;


        public override void Initialize(object data)
        {
            SceneName = ConstantScenes.EXAMPLE_MAIN_MENU;
            LoadingProc = GameManager.Instance.LoadSceneAsync(SceneName);
            LoadingProc.completed += OnLoadSceneCompletedHandler;

            SetInitialize(true);
        }

        public void CloseLoaderWindow()
        {
            Close();
        }


        private void OnLoadSceneCompletedHandler(AsyncOperation operation)
        {
            if (LoadingProc != null)
                LoadingProc.completed -= OnLoadSceneCompletedHandler;

            OnSceneLoaded?.Invoke();

            var windowData = UnityEngine.Random.Range(1, 1001);         //Пример того, что окну можно передавать какие-нибудь данные, н-р, необходимые для отображения
            _window = GameManager.Instance.WindowsController.CreateWindow<MainMenuWindow>(MainMenuWindow.prefabPath, Enums.EnumWindowsLayer.Main, data: windowData);

            if (_window.IsInit)
                OnInitializeHandler();
            else
                _window.OnInitialize += OnInitializeHandler;
        }


        private void OnInitializeHandler()
        {
            _window.OnInitialize -= OnInitializeHandler;
            Close();
        }
    }
}
