using ChampionshipOfBox.Models;
using ChampionshipOfBox.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampionshipOfBox.Services
{
    public class ChampionshipService
    {
        BoxContext db = new BoxContext();
        BattleRepo br = new BattleRepo();

        public IEnumerable<Validater> ChampionshipsValidate(string name, string result)
        {
            bool? res = result == null ? (bool?)null : (result == "true" ? true : false);
            return Championships(name, res) == null ? new List<Validater>() : Championships(name, res);
        }

        // Result winner or loser: Winner - True, Loser - false
        public IEnumerable<Validater> Championships(string name, bool? result)
        {
            if (result.HasValue && !string.IsNullOrEmpty(name))
                return br.Battles().Where(b => result.Value ? b.Winner.Name == name : b.Loser.Name == name)
                    .Select(b =>
                    {
                        return Transform(b);
                    });
            if (string.IsNullOrEmpty(name))
                return br.Battles()
                    .Select(b =>
                    {
                        return Transform(b);
                    });
            return br.Battles().Where(b => b.Winner.Name == name || b.Loser.Name == name)
                .Select(b =>
                {
                    return Transform(b);
                });
        }
               
        private Validater Transform(Battle b) => new Validater { id = b.Id, cell = new[] { b.Date.ToString(), b.AmountRounds.ToString(), b.Winner.Name, b.Loser.Name } };

    }
}