using ChampionshipOfBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampionshipOfBox.Repo
{
    public class BattleRepo
    {
        BoxContext db = new BoxContext();

        public IEnumerable<Battle> Battles() =>
            db.Battles.Join(
                db.Boxers,
                battle => battle.IdWinner,
                boxer => boxer.Id,
                (battle, boxer) => new
                {
                    AmountRounds = battle.AmountRounds,
                    Date = battle.Date,
                    Id = battle.Id,
                    IdLoser = battle.IdLoser,
                    IdWinner = battle.IdWinner,
                    RefereePoints = battle.RefereePoints,
                    Winner = boxer
                }).ToList().Select(battle => new Battle()
                {
                    AmountRounds = battle.AmountRounds,
                    Date = battle.Date,
                    Id = battle.Id,
                    IdLoser = battle.IdLoser,
                    IdWinner = battle.IdWinner,
                    RefereePoints = battle.RefereePoints,
                    Winner = battle.Winner
                })
            .ToList().Join(
                db.Boxers,
                battle => battle.IdLoser,
                boxer => boxer.Id,
                (battle, boxer) => new Battle
                {
                    AmountRounds = battle.AmountRounds,
                    Date = battle.Date,
                    Id = battle.Id,
                    IdLoser = battle.IdLoser,
                    IdWinner = battle.IdWinner,
                    RefereePoints = battle.RefereePoints,
                    Winner = battle.Winner,
                    Loser = boxer
                }).ToList();
    }
}