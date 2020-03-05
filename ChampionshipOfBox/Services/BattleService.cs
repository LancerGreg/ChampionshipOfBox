using ChampionshipOfBox.Failers;
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

        public IEnumerable<Battle> GetBattles() => br.Battles();

        public IEnumerable<Validater> GetValidateBattles()
        {
            return br.Battles()
                .Select(b =>
                {
                    return Transform(b);
                });
        }

        private Validater Transform(Battle b) => new Validater { id = b.Id, cell = new[] { b.Id.ToString(), b.Date.ToString(), b.AmountRounds.ToString(), b.Winner.Id.ToString(), b.Loser.Id.ToString(), b.RefereePoints.ToString() } };


        public Validater BattleValidate(int id) =>
            GetValidateBattles().SingleOrDefault(b => b.id == id);

        public async Task AddNewBattle(ModifyBattleRequest battle)
        {
            if (battle.Date == null)
                throw new ValidationException("The date wasn't right or not post");
            if (battle.AmountOfRounds > 12)
                throw new GreatImportanceException("The amount of rounds cannot be more than 12");
            if (battle.AmountOfRounds <= 0 || battle.Winner <= 0 || battle.Loser <= 0 || battle.RefereePoints < 0)
                throw new NegativeNumberException("Was enter negative or zero params");
            Battle newBattle = new Battle()
            {
                Date = battle.Date.Value,
                AmountRounds = battle.AmountOfRounds.Value,
                IdWinner = battle.Winner.Value,
                IdLoser = battle.Loser.Value,
                RefereePoints = battle.RefereePoints.Value,
                Winner = new Boxer() { Id = battle.Winner.Value },
                Loser = new Boxer() { Id = battle.Loser.Value }
            };
            db.Battles.Add(newBattle);
            await db.SaveChangesAsync();
        }

        public async Task EditBattle(ModifyBattleRequest modifyBattle)
        {
            if (modifyBattle.Date == null)
                throw new ValidationException("The date wasn't right or not post");
            if (modifyBattle.AmountOfRounds > 12)
                throw new GreatImportanceException("The amount of rounds cannot be more than 12");
            if (modifyBattle.AmountOfRounds <= 0 || modifyBattle.Winner <= 0 || modifyBattle.Loser <= 0 || modifyBattle.RefereePoints < 0)
                throw new NegativeNumberException("Was enter negative or zero params");

            var oldBattle = br.Battles().Where(b => b.Id == modifyBattle.Id).SingleOrDefault();

            if (oldBattle != null)
            {
                modifyBattle.Update(oldBattle);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteBattle(int id)
        {
            var battle = new Battle() { Id = id };
            db.Battles.Attach(battle);
            db.Battles.Remove(battle);
            await db.SaveChangesAsync();
        }
    }
}