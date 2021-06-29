﻿using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Constants;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.UI.Windows.DialogExample;
using DllSky.StarterKITv2.UI.Windows.Loading;
using DllSky.StarterKITv2.UI.Windows.MainMenuExample;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows.SecondExampleWindow
{
    public class SecondWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Second\SecondWindow";
        
        private GameManager _gameManager;
        private IWindowSceneLoader _loadingWindow;
        private WindowBase _dialog;


        private void Awake()
        {
            _gameManager = GameManager.Instance;
        }


        public override void Initialize(object data)
        {
            if (_gameManager.CheckCurrentScene(ConstantScenes.EXAMPLE_SECOND_SCENE))
            {
                SetInitialize(true);
            }
            else
            {
                _loadingWindow = _gameManager.WindowsController.CreateWindow<SceneLoadingWindow>(SceneLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading, ConstantScenes.EXAMPLE_SECOND_SCENE);
                _loadingWindow.OnSceneLoaded += OnSceneLoadedHandler;
            }
        }


        public void OnClickShowDialogButton()
        {
            _dialog = _gameManager.WindowsController.CreateWindow<ExampleDialogWindow>(ExampleDialogWindow.prefabPath, Enums.EnumWindowsLayer.Dialogs);
            _dialog.OnClose += OnDialogCloseHandler;
        }

        public void OnClickWindowsQueue()
        { 
            //TODO : добавить код и реализовать саму систему Оконной Очереди
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

        private void OnDialogCloseHandler(bool result, WindowBase dialog)
        {
            if (_dialog)
                _dialog.OnClose += OnDialogCloseHandler;

            Debug.Log("Диалог [" +  dialog.GetType().ToString() + "] закрылся с результатом : " + result.ToString());
        }


        protected override void CustomClose(bool result)
        {
            var windowData = Random.Range(1, 1001);         //Пример того, что окну можно передавать какие-нибудь данные, н-р, необходимые для отображения
            _gameManager.WindowsController.CreateWindow<MainMenuWindow>(MainMenuWindow.prefabPath, Enums.EnumWindowsLayer.Main, data: windowData);
        }        
    }
}
