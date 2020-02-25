using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;
using ChampionshipOfBox.Models;

namespace ChampionshipOfBox.Repo
{
    public class BoxContext : DbContext
    {
        internal DbSet<Battle> Battles { get; set; }
        internal DbSet<Boxer> Boxers { get; set; }
    }
}