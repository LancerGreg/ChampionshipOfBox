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
        BoxService service = new BoxService();

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
                rows = service.ChampionshipsValidate(name, result).ToList()
                //!note! - JSON can accept only and only such data type, do not change the name, accepted data type is only a string
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BattleData(int id)
        {
            var jsonData = new List<Validater>() { service.BattleValidate(id) };
            return PartialView(jsonData);
        }
    }
}