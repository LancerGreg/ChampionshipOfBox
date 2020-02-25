using ChampionshipOfBox.Models;
using ChampionshipOfBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChampionshipOfBox.Controllers
{
    public class BoxController : Controller
    {
        BattleService battleService = new BattleService();

        // GET: MainPage
        public ActionResult Index(string boxerName)
        {
            ViewBag.Ranking = battleService.Rankings(boxerName);
            return View();
        }

        [HttpPut]
        public async Task<string> EditBattle(Battle editBattle)
        {
            await battleService.EditBattle(editBattle);
            return "The Battle has been edit";
        }

        [HttpPost]
        public async Task<string> AddNewBattle(Battle newBattle)
        {
            await battleService.AddNewBattle(newBattle);
            return "A new battle has been registered";
        }
    }
}