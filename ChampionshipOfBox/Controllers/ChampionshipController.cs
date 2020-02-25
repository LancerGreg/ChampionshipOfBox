using ChampionshipOfBox.Models;
using ChampionshipOfBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChampionshipOfBox.Controllers
{
    public class ChampionshipController : Controller
    {
        BattleService battleService = new BattleService();
        // GET: Championship
        public ActionResult Index(Boxer boxer, bool result)
        {
            ViewBag.Championships = battleService.Championships(boxer, result);
            return View();
        }
    }
}