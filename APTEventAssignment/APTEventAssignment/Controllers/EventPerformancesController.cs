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
using APTEventAssignment.ViewModels;

namespace APTEventAssignment.Controllers
{
    [Authorize(Roles = "Admin")]  //only accessible to the admin 
    public class EventPerformancesController : Controller
    {
        private APTEventsEntities db = new APTEventsEntities();

        // GET: EventPerformances
        public ActionResult Index()
        {
            var viewmodel = (from ep in db.EventPerformance
                             join en in db.Event on ep.EventPerformance_EventID equals en.Event_ID

                             select new EventPerformancesViewModel()
                             {
                                 EventPerformance_ID = ep.EventPerformance_ID,
                                 EventPerformance_Date = ep.EventPerformance_Date,
                                 EventPerformance_Time = ep.EventPerformance_Time,
                                 EventPerformance_EventID = en.Event_ID,
                                 EventPerformance_EventName = ep.Event.Event_Name,
                                 
                             });

            return View(viewmodel.ToList());
        }

        // used by the create and edit post methods to map the viewmodel to the model
        private void UpdateEventPerformance(EventPerformance ep, AddEventPerformanceViewModel addviewmodel)
        {
            ep.EventPerformance_EventID = addviewmodel.EventPerformance_EventID;
            ep.EventPerformance_ID = addviewmodel.EventPerformance_ID;
            ep.EventPerformance_Date = (DateTime) addviewmodel.EventPerformance_Date;
            ep.EventPerformance_Time = (TimeSpan) addviewmodel.EventPerformance_Time;
        }

        // GET: EventPerformances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventPerformance ep = db.EventPerformance.Find(id);
            if (ep == null)
            {
                return HttpNotFound();
            }

            var viewmodel = new EventPerformancesViewModel
            {
                EventPerformance_EventName = ep.Event.Event_Name,
                EventPerformance_EventID = ep.Event.Event_ID,
                EventPerformance_ID = ep.EventPerformance_ID,
                EventPerformance_Date = ep.EventPerformance_Date,
                EventPerformance_Time = ep.EventPerformance_Time
            };

            return View(viewmodel);
        }

        // GET: EventPerformances/Create
        public ActionResult Create()
        {
            ViewBag.EventPerformance_EventID = new SelectList(db.Event, "Event_ID", "Event_Name");
            return View();
        }

        // POST: EventPerformances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddEventPerformanceViewModel addviewmodel)
        {
            if (ModelState.IsValid)
            {
                var ep = new EventPerformance();
                UpdateEventPerformance(ep, addviewmodel);

                db.EventPerformance.Add(ep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventPerformance_EventID = new SelectList(db.Event, "Event_ID", "Event_Name", addviewmodel.EventPerformance_EventID);
            return View(addviewmodel);
        }

        // GET: EventPerformances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventPerformance ep = db.EventPerformance.Find(id);
            if (ep == null)
            {
                return HttpNotFound();
            }

            //setting the viewmodel properties to the data from the database of the event with the id passed 
            var addviewmodel = new AddEventPerformanceViewModel
            {
                EventPerformance_EventName = ep.Event.Event_Name,
                EventPerformance_EventID = ep.Event.Event_ID,
                EventPerformance_ID = ep.EventPerformance_ID,
                EventPerformance_Date = ep.EventPerformance_Date,
                EventPerformance_Time = ep.EventPerformance_Time,
            };

            ViewBag.EventPerformance_EventID = new SelectList(db.Event, "Event_ID", "Event_Name", ep.EventPerformance_EventID);
            return View(addviewmodel);
        }

        // POST: EventPerformances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddEventPerformanceViewModel addviewmodel) 
       {
            if (ModelState.IsValid)
            {
                var existingEP = db.EventPerformance.Find(addviewmodel.EventPerformance_ID);
                UpdateEventPerformance(existingEP, addviewmodel);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventPerformance_EventID = new SelectList(db.Event, "Event_ID", "Event_Name", addviewmodel.EventPerformance_EventID);
            return View(addviewmodel);
        }

        // GET: EventPerformances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventPerformance ep = db.EventPerformance.Find(id);
            if (ep == null)
            {
                return HttpNotFound();
            }

            var viewmodel = new EventPerformancesViewModel
            {
                EventPerformance_EventName = ep.Event.Event_Name,
                EventPerformance_EventID = ep.Event.Event_ID,
                EventPerformance_ID = ep.EventPerformance_ID,
                EventPerformance_Date = ep.EventPerformance_Date,
                EventPerformance_Time = ep.EventPerformance_Time,
            };

            return View(viewmodel);
        }

        // POST: EventPerformances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventPerformance ep = db.EventPerformance.Find(id);
            db.EventPerformance.Remove(ep);
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
