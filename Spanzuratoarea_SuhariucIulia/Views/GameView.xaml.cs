using Spanzuratoarea_SuhariucIulia.Models;
using Spanzuratoarea_SuhariucIulia.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Spanzuratoarea_SuhariucIulia.Views
{
    public partial class GameView : Window
    {
        public GameView(User user)
        {
            InitializeComponent();
            DataContext = new GameViewModel(user);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is GameViewModel vm)
            {
                string key = e.Key.ToString();

                if (key.Length == 1 && char.IsLetter(key[0]))
                {
                    char letter = key[0];
                    vm.GuessLetterCommand.Execute(letter);
                }
            }
        }
    }
}