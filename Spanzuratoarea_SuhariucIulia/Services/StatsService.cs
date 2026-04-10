
using Spanzuratoarea_SuhariucIulia.Models;
using System;
using System.IO;
using System.Text.Json;

namespace Spanzuratoarea_SuhariucIulia.Services
{
    public static class StatsService
    {
        private static string GetFilePath(User user) =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{user.Name}_stats.json");

        public static void SaveStats(User user, PlayerStats stats)
        {
            string file = GetFilePath(user);
            var json = JsonSerializer.Serialize(stats);
            File.WriteAllText(file, json);
        }

        public static PlayerStats LoadStats(User user)
        {
            string file = GetFilePath(user);
            if (!File.Exists(file))
                return new PlayerStats();

            var json = File.ReadAllText(file);
            return JsonSerializer.Deserialize<PlayerStats>(json) ?? new PlayerStats();
        }
    }
}