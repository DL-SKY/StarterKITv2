using DllSky.StarterKITv2.Constants;
using DllSky.StarterKITv2.Events;
using DllSky.StarterKITv2.Patterns;
using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows.FPS;
using DllSky.StarterKITv2.UI.Windows.Loading;
using DllSky.StarterKITv2.UI.WindowsManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DllSky.StarterKITv2.Application
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private bool _isUsingFPS;


        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoadedHandler;

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


        //Example
        private void Initialize()
        {
            if (_isUsingFPS)
                CreateFPSCounter();
        }

        private void CreateFPSCounter()
        {
            var windowsManager = ComponentLocator.Resolve<WindowsManager>();
            windowsManager.CreateWindow<GameLoadingWindow>(GameLoadingWindow.prefabPath, Enums.EnumWindowsLayer.Loading);
            windowsManager.CreateWindow<FPSWindow>(FPSWindow.prefabPath, Enums.EnumWindowsLayer.Special);
        }

        private void OnSceneLoadedHandler(Scene scene, LoadSceneMode loadSceneMode)
        {
            Debug.LogWarning("[GameManager] OnSceneLoadedHandler() " + scene.name);
            EventManager.DispatchEvent(ConstantEventsName.ON_SCENE_LOADED, scene.name);
        }
    }
}
