using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spanzuratoarea_SuhariucIulia.Models
{
    public class CategoryStats
    {
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
    }

    public class UserStatistics
    {
        public string UserName { get; set; } = "";
        public Dictionary<string, CategoryStats> ByCategory { get; set; } = new();
    }
}
