﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using APTEventAssignment.Models;

namespace APTEventAssignment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VenueTypesController : Controller
    {
        private APTEventsEntities db = new APTEventsEntities();

        // GET: VenueTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.VenueType.ToListAsync());
        }

        // GET: VenueTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueType venueType = await db.VenueType.FindAsync(id);
            if (venueType == null)
            {
                return HttpNotFound();
            }
            return View(venueType);
        }

        // GET: VenueTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VenueTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VenueType_ID,VenueType_Name,VenueType_Deleted")] VenueType venueType)
        {
            if (ModelState.IsValid)
            {
                db.VenueType.Add(venueType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(venueType);
        }

        // GET: VenueTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueType venueType = await db.VenueType.FindAsync(id);
            if (venueType == null)
            {
                return HttpNotFound();
            }
            return View(venueType);
        }

        // POST: VenueTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VenueType_ID,VenueType_Name,VenueType_Deleted")] VenueType venueType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venueType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(venueType);
        }

        // GET: VenueTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueType venueType = await db.VenueType.FindAsync(id);
            if (venueType == null)
            {
                return HttpNotFound();
            }
            return View(venueType);
        }

        // POST: VenueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VenueType venueType = await db.VenueType.FindAsync(id);
            db.VenueType.Remove(venueType);
            await db.SaveChangesAsync();
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
