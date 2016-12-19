using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ISI.Models;

namespace ISI.Controllers
{
    public class AlquilersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Alquilers
        public ActionResult Index()
        {
            return View(db.Alquilers.ToList());
        }

        // GET: Alquilers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alquiler alquiler = db.Alquilers.Find(id);
            if (alquiler == null)
            {
                return HttpNotFound();
            }
            return View(alquiler);
        }

        // GET: Alquilers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alquilers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlquilerID,PickUpate,DateOfReturn,Cost")] Alquiler alquiler)
        {
            if (ModelState.IsValid)
            {
                db.Alquilers.Add(alquiler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alquiler);
        }

        // GET: Alquilers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alquiler alquiler = db.Alquilers.Find(id);
            if (alquiler == null)
            {
                return HttpNotFound();
            }
            return View(alquiler);
        }

        // POST: Alquilers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlquilerID,PickUpate,DateOfReturn,Cost")] Alquiler alquiler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alquiler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alquiler);
        }

        // GET: Alquilers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alquiler alquiler = db.Alquilers.Find(id);
            if (alquiler == null)
            {
                return HttpNotFound();
            }
            return View(alquiler);
        }

        // POST: Alquilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alquiler alquiler = db.Alquilers.Find(id);
            db.Alquilers.Remove(alquiler);
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
