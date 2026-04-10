using System.Windows;
using Spanzuratoarea_SuhariucIulia.ViewModels;

namespace Spanzuratoarea_SuhariucIulia.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }
    }
}