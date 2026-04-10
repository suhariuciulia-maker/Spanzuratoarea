namespace Spanzuratoarea_SuhariucIulia.Models
{
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int GamesPlayed { get; set; } = 0;
        public int GamesWon { get; set; } = 0;
        public override string ToString()
        {
            return Name;
        }
    }
}