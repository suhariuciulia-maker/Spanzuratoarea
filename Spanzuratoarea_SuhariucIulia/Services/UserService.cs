using Spanzuratoarea_SuhariucIulia.Models;
using System.IO;
using System.Text.Json;

namespace Spanzuratoarea_SuhariucIulia.Services
{
    public class UserService
    {
        private readonly string filePath = "users.json";

        public List<User> LoadUsers()
        {
            if (!File.Exists(filePath))
                return new List<User>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void AddUser(User user)
        {
            var users = LoadUsers();

            if (users.Any(u => u.Name == user.Name))
                throw new Exception("User deja existent!");

            users.Add(user);
            SaveUsers(users);
        }

        public void DeleteUser(User user)
        {
            var users = LoadUsers();
            users.RemoveAll(u => u.Name == user.Name);
            SaveUsers(users);

            // 🔥 IMPORTANT (cerință temă)
            string folder = $"SavedGames/{user.Name}";
            if (Directory.Exists(folder))
                Directory.Delete(folder, true);
        }
    }
}