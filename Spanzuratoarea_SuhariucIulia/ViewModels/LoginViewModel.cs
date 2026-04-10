using Microsoft.Win32;
using Spanzuratoarea_SuhariucIulia.Commands;
using Spanzuratoarea_SuhariucIulia.Models;
using Spanzuratoarea_SuhariucIulia.Services;
using Spanzuratoarea_SuhariucIulia.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Spanzuratoarea_SuhariucIulia.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UserService _userService = new();

        public ObservableCollection<User> Users { get; set; }

        private readonly StatisticsService _statisticsService = new();

        private User? _selectedUser;
        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand PlayCommand { get; }

        public LoginViewModel()
        {
            Users = new ObservableCollection<User>(_userService.LoadUsers());

            NewUserCommand = new RelayCommand(_ => AddUser());
            DeleteUserCommand = new RelayCommand(_ => DeleteUser());
            PlayCommand = new RelayCommand(_ => Play());
        }

        private void AddUser()
        {
            // alegere imagine
            var dialog = new OpenFileDialog
            {
                Filter = "Images (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif"
            };

            if (dialog.ShowDialog() != true)
                return;

            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "Introdu numele utilizatorului:", "New User");

            if (string.IsNullOrWhiteSpace(name))
                return;

            var newUser = new User
            {
                Name = name,
                ImagePath = dialog.FileName
            };

            try
            {
                _userService.AddUser(newUser);
                Users.Add(newUser);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void DeleteUser()
        {
            if (SelectedUser == null) return;

            _userService.DeleteUser(SelectedUser);
            _statisticsService.DeleteUserStatistics(SelectedUser.Name);
            Users.Remove(SelectedUser);
        }

        private void Play()
        {
            if (SelectedUser == null) return;

            var gameView = new GameView(SelectedUser);
            gameView.Show();
        }
    }
}