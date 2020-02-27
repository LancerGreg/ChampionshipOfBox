using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampionshipOfBox.Models
{
    public class Championship
    {
        public DateTime Date { get; set; }
        public int AmountOfRounds { get; set; }
        public string Winner { get; set; }
        public string Loser { get; set; }
    }
}