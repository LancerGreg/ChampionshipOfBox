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
    public class RankingService
    {
        BoxContext db = new BoxContext();
        BattleRepo br = new BattleRepo();

        public IEnumerable<Ranking> Rankings(string boxerName)
        {
            IEnumerable<Ranking> ranking;
            if (boxerName.ToLower() == "all")
                ranking = db.Boxers.Select(b => new Ranking { Boxer = b });
            else
                ranking = db.Boxers.Where(b => b.Name == boxerName).Select(b => new Ranking { Boxer = b });
            if (ranking == null || String.IsNullOrEmpty(boxerName))
                return new List<Ranking>();
            var battles = br.Battles().ToList();

            return ranking.ToList().Select(r =>
            {
                battles.ToList().ForEach(b =>
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