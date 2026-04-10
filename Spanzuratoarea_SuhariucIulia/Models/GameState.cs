namespace Spanzuratoarea_SuhariucIulia.Models
{
    public class GameState
    {
        public string Word { get; set; } = "";
        public string CurrentMask { get; set; } = "";
        public int Mistakes { get; set; }
        public int Level { get; set; }
        public int SecondsLeft { get; set; }
        public string Category { get; set; } = "";
        public List<char> UsedLetters { get; set; } = new();
    }
}