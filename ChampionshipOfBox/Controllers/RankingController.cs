using ChampionshipOfBox.Models;
using ChampionshipOfBox.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChampionshipOfBox.Controllers
{
    public class RankingController : Controller
    {
        RankingService service = new RankingService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MainPage(string name)
        {
            return PartialView(service.Rankings(name).ToList());
        }
    }
}