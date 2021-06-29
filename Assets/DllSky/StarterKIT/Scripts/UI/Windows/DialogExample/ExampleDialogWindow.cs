namespace DllSky.StarterKITv2.UI.Windows.DialogExample
{
    public class ExampleDialogWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Dialog\ExampleDialogWindow";


        public void OnClickOk()
        {
            Close(true);
        }

        public void OnClickCancel()
        {
            Close(false);
        }
    }
}
