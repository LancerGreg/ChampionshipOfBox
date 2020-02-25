using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ChampionshipOfBox.Repo;
using ChampionshipOfBox.Models;

namespace ChampionshipOfBox.Seeder
{
    public class BattleDbInitializer : DropCreateDatabaseAlways<BoxContext>
    {
        protected override void Seed(BoxContext db)
        {
            db.Battles.Add(new Battle { Id = 1, AmountRounds = 12, Date = DateTime.Now, Winner = new Boxer { Id = 1, Name = "Bob" }, Loser = new Boxer { Id = 4, Name = "Stuard" }, RefereePoints = 20 });
            db.Battles.Add(new Battle { Id = 2, AmountRounds = 6, Date = DateTime.Now.AddDays(-20), Winner = new Boxer { Id = 2, Name = "Bob" }, Loser = new Boxer { Id = 5, Name = "Sam" }, RefereePoints = 18 });
            db.Battles.Add(new Battle { Id = 3, AmountRounds = 10, Date = DateTime.Now.AddDays(2), Winner = new Boxer { Id = 3, Name = "Jackson" }, Loser = new Boxer { Id = 6, Name = "Sam" }, RefereePoints = 10 });
            base.Seed(db);
        }
    }
}