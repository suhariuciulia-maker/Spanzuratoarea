using System.IO;
using Spanzuratoarea_SuhariucIulia.Models;
using System.Text.Json;

namespace Spanzuratoarea_SuhariucIulia.Services
{
    public class StatisticsService
    {
        private readonly string filePath = "statistics.json";

        public List<UserStatistics> LoadStatistics()
        {
            if (!File.Exists(filePath))
                return new List<UserStatistics>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<UserStatistics>>(json) ?? new List<UserStatistics>();
        }

        public void SaveStatistics(List<UserStatistics> stats)
        {
            string json = JsonSerializer.Serialize(stats, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }

        public void RegisterFinishedGame(string userName, string category, bool won)
        {
            var allStats = LoadStatistics();

            var userStats = allStats.FirstOrDefault(s => s.UserName == userName);
            if (userStats == null)
            {
                userStats = new UserStatistics
                {
                    UserName = userName
                };
                allStats.Add(userStats);
            }

            if (!userStats.ByCategory.ContainsKey(category))
            {
                userStats.ByCategory[category] = new CategoryStats();
            }

            userStats.ByCategory[category].GamesPlayed++;

            if (won)
                userStats.ByCategory[category].GamesWon++;

            SaveStatistics(allStats);
        }

        public void DeleteUserStatistics(string userName)
        {
            var allStats = LoadStatistics();
            allStats.RemoveAll(s => s.UserName == userName);
            SaveStatistics(allStats);
        }
    }
}