using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampionshipOfBox.Models
{
    public class CreateBattleReq
    {
        public DateTime Date { get; set; }
        public int AmountOfRounds { get; set; }
        public int Winner { get; set; }
        public int Loser { get; set; }
        public int RefereePoints { get; set; }
    }
}