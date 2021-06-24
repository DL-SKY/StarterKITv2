using DllSky.StarterKITv2.Services;
using DllSky.StarterKITv2.UI.Windows.MainMenu;

namespace DllSky.StarterKITv2.UI.Windows.Loading
{
    public class GameLoadingWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Loading\GameLoadingWindow";

        private MainMenuWindow _mainMenu;


        public override void Initialize(object data)
        {
            var windowsManager = ComponentLocator.Resolve<WindowsManager.WindowsManager>();
            _mainMenu = windowsManager.CreateWindow<MainMenuWindow>(MainMenuWindow.prefabPath, Enums.EnumWindowsLayer.Main);
            _mainMenu.OnInitialize += OnInitializeHandler;

            base.Initialize(data);
        }


        private void OnInitializeHandler()
        {
            _mainMenu.OnInitialize -= OnInitializeHandler;
            Close();
        }
    }
}
