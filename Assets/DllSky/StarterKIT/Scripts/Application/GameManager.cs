using DllSky.StarterKITv2.Constants;
using DllSky.StarterKITv2.Events;
using DllSky.StarterKITv2.Interfaces.Windows;
using DllSky.StarterKITv2.Patterns;
using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows.FPS;
using DllSky.StarterKITv2.UI.Windows.Loading;
using DllSky.StarterKITv2.UI.Windows.WindowsQueue;
using DllSky.StarterKITv2.UI.WindowsManager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DllSky.StarterKITv2.Application
{
    public class GameManager : Singleton<GameManager>, IWindowsManagerUsing
    {
        [SerializeField] private bool _isUsingFPS;

        public WindowsManager WindowsController { get; private set; }
        public WindowsQueueController WindowsQueue { get; private set; } 


        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoadedHandler;

            WindowsController = ComponentLocator.Resolve<WindowsManager>();
            WindowsQueue = ComponentLocator.Resolve<WindowsQueueController>();

#if UNITY_ANDROID
            UnityEngine.Application.targetFrameRate = 60;
#endif

            Initialize();
        }


        protected override void CustomOnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoadedHandler;
        }


        public AsyncOperation LoadSceneAsync(string sceneName)
        {
            return SceneManager.LoadSceneAsync(sceneName);
        }

        public bool CheckCurrentScene(string sceneName)
        {
            return SceneManager.GetActiveScene().name == sceneName;
        }


        private void Initialize()
        {
            CreateLoadingWindow();

            //TODO: сделать нормально
            WindowsQueue.SetCheckWindows(new List<System.Type> { typeof(UI.Windows.SecondExampleWindow.SecondWindow) });

            if (_isUsingFPS)
                CreateFPSCounter();
        }

        private void CreateLoadingWindow()
        {
            WindowsController.CreateWindow<GameLoadingWindow>(GameLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading);
        }

        private void CreateFPSCounter()
        {
            WindowsController.CreateWindow<FPSWindow>(FPSWindow.prefabPath, Enums.EnumWindowsLayer.Special, includeInWindowsList: false);
        }

        private void OnSceneLoadedHandler(Scene scene, LoadSceneMode loadSceneMode)
        {
            Debug.LogWarning("[GameManager] OnSceneLoadedHandler() " + scene.name);
            EventManager.DispatchEvent(ConstantEventsName.ON_SCENE_LOADED, scene.name);
        }
    }
}
