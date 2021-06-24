using DllSky.StarterKITv2.Application;
using DllSky.StarterKITv2.Constants;
using UnityEngine;

namespace DllSky.StarterKITv2.UI.Windows.MainMenu
{
    public class MainMenuWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\MainMenu\MainMenuWindow";

        private object _data;
        private GameManager _gameManager;
        private AsyncOperation _loading;


        private void Awake()
        {
            _gameManager = GameManager.Instance;
        }


        public override void Initialize(object data)
        {
            _data = data;

            if (_gameManager.CheckCurrentScene(ConstantScenes.EXAMPLE_MAIN_MENU))
            {
                base.Initialize(data);
            }
            else
            {
                _loading = _gameManager.LoadSceneAsync(ConstantScenes.EXAMPLE_MAIN_MENU);
                _loading.completed += OnLoadSceneCompletedHandler;
            }
        }

        //public void OnClickTest()
        //{
        //    var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
        //    windowsManager.CreateWindow<TestShootingWindow>(TestShootingWindow.prefabPath, Enums.EnumWindowsLayer.Main);

        //    Close();
        //}


        private void OnLoadSceneCompletedHandler(AsyncOperation operation)
        {
            if (_loading != null)
                _loading.completed -= OnLoadSceneCompletedHandler;

            base.Initialize(_data);
        }
    }
}
