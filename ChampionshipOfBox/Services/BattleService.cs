using ChampionshipOfBox.Models;
using ChampionshipOfBox.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChampionshipOfBox.Services
{
    public class BattleService
    {
        BoxContext db = new BoxContext();
        BattleRepo br = new BattleRepo();

        public async Task AddNewBattle(Battle newBattle)
        {
            db.Battles.Add(newBattle);
            await db.SaveChangesAsync();
        }

        public async Task EditBattle(Battle editBattle)
        {
            var oldBattle = br.Battles().Where(b => b.Id == editBattle.Id).SingleOrDefault();

            if (oldBattle != null)
            {
                oldBattle = editBattle;
                await db.SaveChangesAsync();
            }
        }

        public Validater BattleValidate(int id) =>
            br.Battles().Select(b => new Validater()
            {
                id = b.Id,
                cell = new[] { b.Id.ToString(), b.Date.ToString(), b.AmountRounds.ToString(), b.Winner.Name, b.Loser.Name, b.RefereePoints.ToString() }
            }).SingleOrDefault(b => b.id == id);

        public Battle GetBattle(int id) => br.Battles().SingleOrDefault(b => b.Id == id);
    }
}