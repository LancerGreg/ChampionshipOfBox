using ChampionshipOfBox.Models;
using ChampionshipOfBox.Repo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChampionshipOfBox.Services
{
    public class BoxService
    {
        BoxContext db = new BoxContext();

        public async Task AddNewBattle(Battle newBattle)
        {
            db.Battles.Add(newBattle);
            await db.SaveChangesAsync();
        }

        public async Task EditBattle(Battle editBattle)
        {
            var oldBattle = Battles().Where(b => b.Id == editBattle.Id).SingleOrDefault();

            if (oldBattle != null)
            {
                oldBattle = editBattle;
                await db.SaveChangesAsync();
            }
        }

        // Result winner or loser: Winner - True, Loser - false
        public IEnumerable<Battle> Championships(Boxer boxer, bool? result)
        {
            if (result.HasValue && boxer != null)
                return Battles().Where(b => result.Value ? b.Winner == boxer : b.Loser == boxer);
            if (boxer == null)
                return Battles();
            return Battles().Where(b => b.Winner == boxer || b.Loser == boxer);
        }

        public IEnumerable<Ranking> Rankings(string boxerName)
        {
            var ranking = new List<Ranking>();
            if (boxerName == "All")
                ranking = db.Boxers.Select(b => new Ranking { Boxer = b }).ToList();
            else
                ranking = db.Boxers.Where(b => b.Name == boxerName).Select(b => new Ranking { Boxer = b }).ToList();
            if (ranking == null || String.IsNullOrEmpty(boxerName))
                return new List<Ranking>();
            var battles = Battles().ToList();

            return ranking.Select(r =>
            {
                battles.ToList().ForEach(b =>
                {
                    if (b.Winner.Id == r.Boxer.Id)
                    {
                        r.AmountBottles++;
                        r.CurrentRanking += b.RefereePoints;
                    }
                    if (b.Loser.Id == r.Boxer.Id)
                        r.AmountBottles++;
                });

                return r;
            });
        }

        // working crutch
        //private async Task<IEnumerable<Battle>> Battles() =>
        //    (await db.Battles.ToListAsync()).Where(b => db.Boxers.Any(r => r.Id == b.IdWinner || r.Id == b.IdLoser)).Select(b =>
        //    {
        //        db.Boxers.ToList().ForEach(r =>
        //        {
        //            if (r.Id == b.IdWinner)
        //                b.Winner = r;
        //            if (r.Id == b.IdLoser)
        //                b.Loser = r;
        //        });
        //        return b;
        //    });

        private IEnumerable<Battle> Battles() =>
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
                battle => battle.IdWinner,
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