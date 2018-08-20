using StatementViewer.Utilities;
using System.Windows;

namespace StatementViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Start();
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            Logger.Stop();
            base.OnExit(e);
        }
    }
}
