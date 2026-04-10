using Spanzuratoarea_SuhariucIulia.Commands;
using Spanzuratoarea_SuhariucIulia.Models;
using Spanzuratoarea_SuhariucIulia.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;

namespace Spanzuratoarea_SuhariucIulia.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private readonly Random _random = new();
        private readonly GameSaveService _saveService = new();
        private readonly WordService _wordService = new();
        private DispatcherTimer? _timer;

        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string CurrentUser { get; set; }

        public string CurrentWord { get; set; } = "";

        public ICommand CancelCommand { get; }
        public ICommand AboutCommand { get; }

        private readonly StatisticsService _statisticsService = new();
        public ObservableCollection<char> Alphabet { get; set; }
        public ObservableCollection<char> UsedLetters { get; set; } = new();
        public ObservableCollection<string> Categories { get; set; }

        private string _selectedCategory = "All";
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                StartNewGame();
            }
        }

        private string _displayWord = "";
        public string DisplayWord
        {
            get => _displayWord;
            set
            {
                _displayWord = value;
                OnPropertyChanged();
            }
        }

        private int _mistakes;
        public int Mistakes
        {
            get => _mistakes;
            set
            {
                _mistakes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HangmanImage));
            }
        }
        private int _level;
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

        private int _secondsLeft;
        public int SecondsLeft
        {
            get => _secondsLeft;
            set
            {
                _secondsLeft = value;
                OnPropertyChanged();
            }
        }
       
        public ICommand StatisticsCommand { get; }
        public ICommand GuessLetterCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand SaveGameCommand { get; }
        public ICommand OpenGameCommand { get; }
        public string HangmanImage
        {
            get
            {
                return "D:\\AN2\\SEM2\\MVP\\Spanzuratoarea\\Spanzuratoarea_SuhariucIulia\\Resources\\Images\\hang"
                       + Mistakes + ".png";
            }
        }
        public GameViewModel(User user)
        {
            CurrentUser = user.Name;
            UserName = user.Name;
            UserImage = user.ImagePath;

            Alphabet = new ObservableCollection<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            Categories = new ObservableCollection<string>(_wordService.GetCategories());

            GuessLetterCommand = new RelayCommand(p => GuessLetter((char)p!));
            NewGameCommand = new RelayCommand(_ => StartNewGame());
            SaveGameCommand = new RelayCommand(_ => SaveGame());
            OpenGameCommand = new RelayCommand(_ => OpenGame());
            StatisticsCommand = new RelayCommand(_ => OpenStatistics());
            CancelCommand = new RelayCommand(_ => Cancel());
            AboutCommand = new RelayCommand(_ => ShowAbout());
            SelectedCategory = Categories.Contains("All") ? "All" : Categories.FirstOrDefault() ?? "";
            StartNewGame();
        }


        private void StartNewGame()
        {
            var words = _wordService.GetWords(SelectedCategory);

            if (!words.Any())
            {
                System.Windows.MessageBox.Show("Nu există cuvinte în categoria selectată.");
                return;
            }

            CurrentWord = words[_random.Next(words.Count)];
            DisplayWord = string.Join(" ", Enumerable.Repeat("_", CurrentWord.Length));

            Mistakes = 0;
            Level = 0;
            UsedLetters.Clear();

            StartTimer();
        }

        private void OpenStatistics()
        {
            var window = new Spanzuratoarea_SuhariucIulia.Views.StatisticsView();
            window.ShowDialog();
        }
        private void StartNewRound()
        {
            var words = _wordService.GetWords(SelectedCategory);

            if (!words.Any())
            {
                System.Windows.MessageBox.Show("Nu există cuvinte în categoria selectată.");
                return;
            }

            CurrentWord = words[_random.Next(words.Count)];
            DisplayWord = string.Join(" ", Enumerable.Repeat("_", CurrentWord.Length));

            Mistakes = 0;
            UsedLetters.Clear();

            StartTimer();
        }

        private void GuessLetter(char letter)
        {
            if (UsedLetters.Contains(letter))
                return;

            UsedLetters.Add(letter);

            bool found = false;
            char[] display = DisplayWord.Replace(" ", "").ToCharArray();

            for (int i = 0; i < CurrentWord.Length; i++)
            {
                if (CurrentWord[i] == letter)
                {
                    display[i] = letter;
                    found = true;
                }
            }

            DisplayWord = string.Join(" ", display);

            if (!found)
                Mistakes++;

            CheckGameStatus();
        }

        private void CheckGameStatus()
        {
            if (!DisplayWord.Contains('_'))
            {
                Level++;

                if (Level == 3)
                {
                    EndGame(true);
                    return;
                }

                System.Windows.MessageBox.Show("Nivel câștigat!");
                StartNewRound();
                return;
            }

            if (Mistakes >= 6)
            {
                EndGame(false);
            }
        }

        private void EndGame(bool win)
        {
            _timer?.Stop();

            _statisticsService.RegisterFinishedGame(CurrentUser, SelectedCategory, win);

            if (win)
                System.Windows.MessageBox.Show("Ai câștigat jocul!");
            else
                System.Windows.MessageBox.Show($"Ai pierdut! Cuvântul era: {CurrentWord}");

            StartNewGame();
        }

        private void StartTimer()
        {
            _timer?.Stop();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            SecondsLeft = 30;

            _timer.Tick += (s, e) =>
            {
                SecondsLeft--;

                if (SecondsLeft <= 0)
                {
                    _timer?.Stop();
                    EndGame(false);
                }
            };

            _timer.Start();
        }

        private void SaveGame()
        {
            var state = new GameState
            {
                Word = CurrentWord,
                CurrentMask = DisplayWord,
                Mistakes = Mistakes,
                Level = Level,
                SecondsLeft = SecondsLeft,
                Category = SelectedCategory,
                UsedLetters = UsedLetters.ToList()
            };

            _saveService.SaveGame(CurrentUser, state);
            System.Windows.MessageBox.Show("Joc salvat!");
        }

        private void OpenGame()
        {
            var files = _saveService.GetSavedGames(CurrentUser);

            if (!files.Any())
            {
                System.Windows.MessageBox.Show("Nu există salvări!");
                return;
            }

            var state = _saveService.LoadGame(files.Last());

            SelectedCategory = state.Category;
            CurrentWord = state.Word;
            DisplayWord = state.CurrentMask;
            Mistakes = state.Mistakes;
            Level = state.Level;
            SecondsLeft = state.SecondsLeft;

            UsedLetters.Clear();
            foreach (var l in state.UsedLetters)
                UsedLetters.Add(l);

            _timer?.Stop();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += (s, e) =>
            {
                SecondsLeft--;

                if (SecondsLeft <= 0)
                {
                    _timer?.Stop();
                    EndGame(false);
                }
            };

            _timer.Start();

            System.Windows.MessageBox.Show("Joc încărcat!");
        }
        private void Cancel()
        {
            var login = new Spanzuratoarea_SuhariucIulia.Views.LoginView();
            login.Show();

            var currentWindow = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w is Spanzuratoarea_SuhariucIulia.Views.GameView);

            currentWindow?.Close();
        }
        private void ShowAbout()
        {
            MessageBox.Show(
                "Nume: Suhariuc Iulia\nGrupa: 10LF343\nSpecializare: Informatica Aplicata",
                "About",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}