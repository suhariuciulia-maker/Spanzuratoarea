using Spanzuratoarea_SuhariucIulia.Models;
using Spanzuratoarea_SuhariucIulia.Services;
using System.Collections.ObjectModel;

namespace Spanzuratoarea_SuhariucIulia.ViewModels
{
    public class StatisticsRow
    {
        public string UserName { get; set; } = "";
        public string Category { get; set; } = "";
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
    }

    public class StatisticsViewModel : BaseViewModel
    {
        private readonly StatisticsService _statisticsService = new();

        public ObservableCollection<StatisticsRow> Rows { get; set; } = new();

        public StatisticsViewModel()
        {
            Load();
        }

        private void Load()
        {
            var stats = _statisticsService.LoadStatistics();

            Rows.Clear();

            foreach (var user in stats)
            {
                foreach (var category in user.ByCategory)
                {
                    Rows.Add(new StatisticsRow
                    {
                        UserName = user.UserName,
                        Category = category.Key,
                        GamesPlayed = category.Value.GamesPlayed,
                        GamesWon = category.Value.GamesWon
                    });
                }
            }
        }
    }
}