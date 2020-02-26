using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;
using ChampionshipOfBox.Models;

namespace ChampionshipOfBox.Repo
{
    public class BoxContext : DbContext
    {
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Boxer> Boxers { get; set; }
    }
}