using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampionshipOfBox.Models
{
    public class Ranking
    {
        public Boxer Boxer { get; set; }
        public int CurrentRanking { get; set; }
        public int AmountBottles { get; set; }
    }
}