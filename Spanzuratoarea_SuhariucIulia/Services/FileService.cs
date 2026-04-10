using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Spanzuratoarea_SuhariucIulia.Models;

namespace Spanzuratoarea_SuhariucIulia.Services
{
    public class FileService
    {
        private readonly string _usersPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "users.json");

        public List<User> LoadUsers()
        {
            if (!File.Exists(_usersPath)) return new List<User>();

            try
            {
                string json = File.ReadAllText(_usersPath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch
            {
                return new List<User>();
            }
        }

        public void SaveUsers(List<User> users)
        {
            string dir = Path.GetDirectoryName(_usersPath);
            if (dir != null && !Directory.Exists(dir)) Directory.CreateDirectory(dir);

            string json = JsonSerializer.Serialize(users);
            File.WriteAllText(_usersPath, json);
        }

        public List<string> LoadWords(string category)
        {
            return new List<string> { "PROGRAMARE", "WPF", "DOTNET" };
        }
    }
}