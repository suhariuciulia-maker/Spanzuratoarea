using Spanzuratoarea_SuhariucIulia.Models;
using System.IO;
using System.Text.Json;

namespace Spanzuratoarea_SuhariucIulia.Services
{
    public class GameSaveService
    {
        private string GetUserFolder(string username)
        {
            string path = $"SavedGames/{username}";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        public void SaveGame(string username, GameState state)
        {
            string folder = GetUserFolder(username);

            string fileName = $"save_{DateTime.Now:yyyyMMdd_HHmmss}.json";
            string fullPath = Path.Combine(folder, fileName);

            string json = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fullPath, json);
        }

        public List<string> GetSavedGames(string username)
        {
            string folder = GetUserFolder(username);

            return Directory.GetFiles(folder, "*.json").ToList();
        }

        public GameState LoadGame(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<GameState>(json)!;
        }
    }
}