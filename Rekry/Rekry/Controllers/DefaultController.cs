using Rekry.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

            if (Request.HttpMethod == "POST")
            {
                var ddlvalue = Request["Tuotenumero"];
                IEnumerable<Tuote> haku = db.Database.SqlQuery<Tuote>("SELECT * FROM dbo.Tuote WHERE Tuotenumero = @p0", ddlvalue);

                return View(haku);
            }

            return View(getTuotteet);
        }

        public ActionResult Muokkaa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuote tuote = db.Tuote.Find(id);
            if (tuote == null)
            {
                return HttpNotFound();
            }
            return View(tuote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Muokkaa([Bind(Include = "Tuotenumero,Kategoria,Nimi,Hinta,Kuvaus,Muokattu")] Tuote tuote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/Index");
            }
            return View(tuote);
        }

        public ActionResult Poista(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuote tuote = db.Tuote.Find(id);
            if (tuote == null)
            {
                return HttpNotFound();
            }
            return View(tuote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Poista(int id)
        {
            Tuote tuote = db.Tuote.Find(id);
            db.Tuote.Remove(tuote);
            db.SaveChanges();
            return RedirectToAction("/Index");
        }

        public ActionResult Luo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Luo([Bind(Include = "Tuotenumero,Kategoria,Nimi,Hinta,Kuvaus,Muokattu")] Tuote tuote)
        {
            if (ModelState.IsValid)
            {
                db.Tuote.Add(tuote);
                db.SaveChanges();
                return RedirectToAction("/Index");
            }

            return View(tuote);
        }
    }
}