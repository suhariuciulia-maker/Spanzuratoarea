using System.IO;
using System.Text.Json;

namespace Spanzuratoarea_SuhariucIulia.Services
{
    public class WordService
    {
        private readonly string filePath = "words.json";
        private Dictionary<string, List<string>> _data;

        public WordService()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                _data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json)
                        ?? new Dictionary<string, List<string>>();
            }
            else
            {
                _data = new Dictionary<string, List<string>>();
            }
        }

        public List<string> GetCategories()
        {
            return _data.Keys.ToList();
        }

        public List<string> GetWords(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return new List<string>();

            if (category == "All")
            {
                return _data
                    .Where(k => k.Key != "All")
                    .SelectMany(k => k.Value)
                    .ToList();
            }

            return _data.ContainsKey(category) ? _data[category] : new List<string>();
        }
    }
}