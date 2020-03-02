using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampionshipOfBox.Models
{
    public class ModifyBattleRequest
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? AmountOfRounds { get; set; }
        public int? Winner { get; set; }
        public int? Loser { get; set; }
        public int? RefereePoints { get; set; }

        public void Update(Battle battle)
        {
            battle.Date = Date ?? battle.Date;
            battle.AmountRounds = AmountOfRounds ?? battle.AmountRounds;
            battle.IdWinner = Winner ?? battle.IdWinner;
            battle.IdLoser = Loser ?? battle.IdLoser;
            battle.RefereePoints = RefereePoints ?? battle.RefereePoints;
        }
    }
}