using System.Windows;

namespace ProjectOne
{
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow mainWindow = new MainWindow();
            Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
