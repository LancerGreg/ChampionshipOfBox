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
    public class BattleService
    {
        BoxContext db = new BoxContext();

        public async Task AddNewBattle(Battle newBattle)
        {
            db.Battles.Add(newBattle);
            await db.SaveChangesAsync();
        }

        public async Task EditBattle(Battle editBattle)
        {
            var oldBattle = await db.Battles.Where(b => b.Id == editBattle.Id).SingleOrDefaultAsync();

            if (oldBattle != null)
            {
                oldBattle = editBattle;
                await db.SaveChangesAsync();
            }
        }

        // Result winner or loser: Winner - True, Loser - false
        public async Task<IEnumerable<Battle>> Championships(Boxer boxer, bool? result)
        {
            if (result.HasValue)
                return await db.Battles.Where(b => result.Value ? b.Winner == boxer : b.Loser == boxer).ToListAsync();
            if (boxer == null)
                return await db.Battles.ToListAsync();
            return await db.Battles.Where(b => b.Winner == boxer || b.Loser == boxer).ToListAsync();
        }

        public async Task<IEnumerable<Ranking>> Rankings(string boxerName)
        {
            var ranking = await db.Boxers.Select(b => new Ranking { Boxer = b }).ToListAsync();
            if (ranking == null)
                return new List<Ranking>();
            var battles = await db.Battles.Where(b => b.Winner.Name == boxerName || b.Loser.Name == boxerName).ToListAsync();

            return ranking.Select(r =>
            {
                battles.ForEach(b =>
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
    }
}