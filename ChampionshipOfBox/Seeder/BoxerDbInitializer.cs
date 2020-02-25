using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ChampionshipOfBox.Repo;
using ChampionshipOfBox.Models;

namespace ChampionshipOfBox.Seeder
{
    public class BoxerDbInitializer : DropCreateDatabaseAlways<BoxContext>
    {
        protected override void Seed(BoxContext db)
        {
            db.Boxers.Add(new Boxer { Id = 1, Name = "Bob" });
            db.Boxers.Add(new Boxer { Id = 2, Name = "Bob" });
            db.Boxers.Add(new Boxer { Id = 3, Name = "Jackson" });
            db.Boxers.Add(new Boxer { Id = 4, Name = "Stuard" });
            db.Boxers.Add(new Boxer { Id = 5, Name = "Sam" });
            db.Boxers.Add(new Boxer { Id = 6, Name = "Sam" });
            base.Seed(db);
        }
    }
}