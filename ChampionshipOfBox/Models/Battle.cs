using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampionshipOfBox.Models
{
    public class Battle
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AmountRounds { get; set; }
        public int IdWinner { get; set; }
        public Boxer Winner { get; set; }
        public int IdLoser { get; set; }
        public Boxer Loser { get; set; }
        public int RefereePoints { get; set; }
    }
}