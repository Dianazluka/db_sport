using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sport.Models;

namespace sport.Controllers
{
    [Authorize]
    public class ParticipationsController : Controller
    {
        private sportEntities db = new sportEntities();
        [AllowAnonymous]
        // GET: Participations
        public ActionResult Index()
        {
            var participations = db.Participations.Include(p => p.Competition).Include(p => p.Sportsman);
            return View(participations.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Index(string search)
        {
            var result = db.Participations
            .Include(p => p.p_сompetition_ID)
            .Include(p => p.p_sportsman_ID)
            .Where(p => p.result.ToString().Contains(search.ToLower())
            || p.place.ToString().Contains(search.ToLower()))
            .ToList();
            return View(result);
        }
        // GET: Participations/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participation participation = db.Participations.Find(id);
            if (participation == null)
            {
                return HttpNotFound();
            }
            return View(participation);
        }

        // GET: Participations/Create
        public ActionResult Create()
        {
            ViewBag.p_сompetition_ID = new SelectList(db.Competitions, "сompetition_ID", "number_participants");
            ViewBag.p_sportsman_ID = new SelectList(db.Sportsmen, "sportsman_ID", "surname");
            return View();
        }

        // POST: Participations/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "participation_ID,p_sportsman_ID,p_сompetition_ID,result,place")] Participation participation)
        {
            if (ModelState.IsValid)
            {
                db.Participations.Add(participation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.p_сompetition_ID = new SelectList(db.Competitions, "сompetition_ID", "number_participants", participation.p_сompetition_ID);
            ViewBag.p_sportsman_ID = new SelectList(db.Sportsmen, "sportsman_ID", "surname", participation.p_sportsman_ID);
            return View(participation);
        }

        // GET: Participations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participation participation = db.Participations.Find(id);
            if (participation == null)
            {
                return HttpNotFound();
            }
            ViewBag.p_сompetition_ID = new SelectList(db.Competitions, "сompetition_ID", "number_participants", participation.p_сompetition_ID);
            ViewBag.p_sportsman_ID = new SelectList(db.Sportsmen, "sportsman_ID", "surname", participation.p_sportsman_ID);
            return View(participation);
        }

        // POST: Participations/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "participation_ID,p_sportsman_ID,p_сompetition_ID,result,place")] Participation participation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.p_сompetition_ID = new SelectList(db.Competitions, "сompetition_ID", "number_participants", participation.p_сompetition_ID);
            ViewBag.p_sportsman_ID = new SelectList(db.Sportsmen, "sportsman_ID", "surname", participation.p_sportsman_ID);
            return View(participation);
        }

        // GET: Participations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participation participation = db.Participations.Find(id);
            if (participation == null)
            {
                return HttpNotFound();
            }
            return View(participation);
        }

        // POST: Participations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Participation participation = db.Participations.Find(id);
            db.Participations.Remove(participation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
