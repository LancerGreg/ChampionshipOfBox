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
        public IEnumerable<Validater> Championships(string name, bool? result)
        {
            if (result.HasValue && !string.IsNullOrEmpty(name))
                return Battles().Where(b => result.Value ? b.Winner.Name == name : b.Loser.Name == name)
                    .Select(b =>
                    {
                        return Transform(b);
                    });
            if (string.IsNullOrEmpty(name))
                return Battles()
                    .Select(b =>
                    {
                        return Transform(b);
                    });
            return Battles().Where(b => b.Winner.Name == name || b.Loser.Name == name)
                .Select(b =>
                {
                    return Transform(b);
                });
        }

        private Validater Transform(Battle b) => new Validater { id = b.Id, cell = new[] { b.Date.ToString(), b.AmountRounds.ToString(), b.Winner.Name, b.Loser.Name } };

        public IEnumerable<Validater> ChampionshipsValidate(string name, string result)
        {
            bool? res = result == null ? (bool?)null : (result == "true" ? true : false);
            return Championships(name, res) == null ? new List<Validater>() : Championships(name, res);
        }

        public Validater BattleValidate(int id) =>
            Battles().Select(b => new Validater() { 
                id = b.Id, 
                cell = new[] { b.Id.ToString(), b.Date.ToString(), b.AmountRounds.ToString(), b.Winner.Name, b.Loser.Name, b.RefereePoints.ToString() } 
            }).SingleOrDefault(b => b.id == id);

        public Battle GetBattle(int id) => Battles().SingleOrDefault(b => b.Id == id);

        public IEnumerable<Ranking> Rankings(string boxerName)
        {
            IEnumerable<Ranking> ranking;
            if (boxerName.ToLower() == "all")
                ranking = db.Boxers.Select(b => new Ranking { Boxer = b });
            else
                ranking = db.Boxers.Where(b => b.Name == boxerName).Select(b => new Ranking { Boxer = b });
            if (ranking == null || String.IsNullOrEmpty(boxerName))
                return new List<Ranking>();
            var battles = Battles().ToList();

            return ranking.ToList().Select(r =>
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