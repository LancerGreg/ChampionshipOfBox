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

        public Battle GetBattle(int id) => br.Battles().SingleOrDefault(b => b.Id == id);

        public IEnumerable<Battle> GetBattles() => db.Battles;

        public IEnumerable<Validater> GetValidateBattles()
        {
            return br.Battles()
                .Select(b =>
                {
                    return Transform(b);
                });
        }

        private Validater Transform(Battle b) => new Validater { id = b.Id, cell = new[] { b.Id.ToString(), b.Date.ToString(), b.AmountRounds.ToString(), b.Winner.Name, b.Loser.Name, b.RefereePoints.ToString() } };


        public Validater BattleValidate(int id) =>
            GetValidateBattles().SingleOrDefault(b => b.id == id);

        public async Task<Battle> AddNewBattle(CreateBattleReq battle)
        {
            Battle newBattle = new Battle()
            {
                Date = DateTime.Now,
                AmountRounds = battle.AmountOfRounds,
                IdWinner = battle.Winner,
                IdLoser = battle.Loser,
                RefereePoints = battle.RefereePoints
            };
            db.Battles.Add(newBattle);
            await db.SaveChangesAsync();
            return newBattle;
        }

        public async Task<Battle> EditBattle(ModifyBattleRequest modifyBattle)
        {
            var oldBattle = br.Battles().Where(b => b.Id == modifyBattle.Id).SingleOrDefault();

            if (oldBattle != null)
            {
                modifyBattle.Update(oldBattle);
                await db.SaveChangesAsync();

                return oldBattle;
            }
            return null;
        }

        public async Task DeleteBattle(int id)
        {
            var battle = GetBattle(id);
            db.Battles.Remove(battle);
            await db.SaveChangesAsync();
        }
    }
}