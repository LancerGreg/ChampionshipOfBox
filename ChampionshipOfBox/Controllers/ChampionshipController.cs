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
        public ActionResult ChampionshipData(/*string name, bool? result*/)
        {
            var jsonData = new
            {                
                rows = service.ChampionshipsValidate(/*name, result*/null, null).ToList()
                //!note! - JSON can accept only and only such data type, do not change the name, accepted data type is only a string
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}