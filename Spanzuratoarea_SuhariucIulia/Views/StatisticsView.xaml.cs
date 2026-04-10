using Spanzuratoarea_SuhariucIulia.ViewModels;
using System.Windows;

namespace Spanzuratoarea_SuhariucIulia.Views
{
    public partial class StatisticsView : Window
    {
        public StatisticsView()
        {
            InitializeComponent();
            DataContext = new StatisticsViewModel();
        }
    }
}