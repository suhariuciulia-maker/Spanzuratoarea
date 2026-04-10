using System.Windows;
using Spanzuratoarea_SuhariucIulia.Views;

namespace Spanzuratoarea_SuhariucIulia
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var loginView = new LoginView();
            loginView.DataContext = new Spanzuratoarea_SuhariucIulia.ViewModels.LoginViewModel();
            loginView.Show();
        }
    }
}