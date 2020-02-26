using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ChampionshipOfBox.Repo;
using ChampionshipOfBox.Models;

namespace ChampionshipOfBox.Seeder
{
    public class BoxDbInitializer : DropCreateDatabaseAlways<BoxContext>
    {
        protected override void Seed(BoxContext db)
        {
            db.Boxers.Add(new Boxer { Id = 1, Name = "Bob" });
            db.Boxers.Add(new Boxer { Id = 2, Name = "Bob" });
            db.Boxers.Add(new Boxer { Id = 3, Name = "Jackson" });
            db.Boxers.Add(new Boxer { Id = 4, Name = "Stuard" });
            db.Boxers.Add(new Boxer { Id = 5, Name = "Sam" });
            db.Boxers.Add(new Boxer { Id = 6, Name = "Sam" });
            db.Battles.Add(new Battle { Id = 1, AmountRounds = 12, Date = DateTime.Now, IdWinner = 1, IdLoser = 4, RefereePoints = 20 });
            db.Battles.Add(new Battle { Id = 2, AmountRounds = 6, Date = DateTime.Now.AddDays(-20), IdWinner = 2, IdLoser = 5, RefereePoints = 18 });
            db.Battles.Add(new Battle { Id = 3, AmountRounds = 10, Date = DateTime.Now.AddDays(2), IdWinner = 3, IdLoser = 6, RefereePoints = 10 });

            base.Seed(db);
        }
    }
}