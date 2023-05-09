using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NRL_Ladder_FrontEnd_v2.Models;

namespace NRL_Ladder_FrontEnd_v2.Controllers
{
    public class LaddersController : Controller
    {
        private NRLDatabaseEntities db = new NRLDatabaseEntities();

        // GET: Ladders
        public ActionResult Index()
        {
            var ladders = db.Ladders.Include(l => l.Season).Include(l => l.Team);
            return View(ladders.ToList());
        }

        // GET: Ladders/Details/5
        public ActionResult Details(string seasonID, string teamName)
        {
            if (seasonID == null || teamName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ladder ladder = db.Ladders.Find(seasonID, teamName);
            if (ladder == null)
            {
                return HttpNotFound();
            }
            return View(ladder);
        }

        // GET: Ladders/Create
        public ActionResult Create()
        {
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "SeasonID");
            ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "TeamName");
            return View();
        }

        // POST: Ladders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeasonID,TeamName,Rank,Points,Wins,Drawn,Lost,Byes,For,Against,Differential")] Ladder ladder)
        {
            if (ModelState.IsValid)
            {
                db.Ladders.Add(ladder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "SeasonID", ladder.SeasonID);
            ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "TeamName", ladder.TeamName);
            return View(ladder);
        }

        // GET: Ladders/Edit/5
        public ActionResult Edit(string seasonID, string teamName)
        {
            if (seasonID == null || teamName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ladder ladder = db.Ladders.Find(seasonID, teamName);
            if (ladder == null)
            {
                return HttpNotFound();
            }
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "SeasonID", ladder.SeasonID);
            ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "TeamName", ladder.TeamName);
            return View(ladder);
        }

        // POST: Ladders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeasonID,TeamName,Rank,Points,Wins,Drawn,Lost,Byes,For,Against,Differential")] Ladder ladder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ladder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "SeasonID", ladder.SeasonID);
            ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "TeamName", ladder.TeamName);
            return View(ladder);
        }

        // GET: Ladders/Delete/5
        public ActionResult Delete(string seasonID, string teamName)
        {
            if (seasonID == null || teamName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ladder ladder = db.Ladders.Find(seasonID, teamName);
            if (ladder == null)
            {
                return HttpNotFound();
            }
            return View(ladder);
        }

        // POST: Ladders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Ladder ladder = db.Ladders.Find(id);
            db.Ladders.Remove(ladder);
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
