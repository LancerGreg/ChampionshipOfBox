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
        ChampionshipService championshipService = new ChampionshipService();
        BattleService battlepService = new BattleService();

        // GET: Championship
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult ChampionshipData(string name, string result)
        {
            var jsonData = new
            {                
                rows = championshipService.ChampionshipsValidate(name, result).ToList()
                //!note! - JSON can accept only and only such data type, do not change the name, accepted data type is only a string
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BattleData(int id)
        {
            var jsonData = new List<Validater>() { battlepService.BattleValidate(id) };
            return PartialView(jsonData);
        }
    }
}