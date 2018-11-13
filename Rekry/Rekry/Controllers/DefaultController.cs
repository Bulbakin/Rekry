using Rekry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rekry.Controllers
{
    public class DefaultController : Controller
    {
        private RekryEntities db = new RekryEntities();

        // GET: Default
        public ActionResult Index()
        {
            var getTuotteet = db.Tuote.ToList();
            SelectList list = new SelectList(getTuotteet, "Tuotenumero", "Tuotenumero");
            ViewBag.Tuote = list;

            return View(getTuotteet);
        }
    }
}