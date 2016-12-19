using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ISI.Models;
using System.Web.Security;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace ISI.Controllers
{
    public class EstadisticasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Estadisticas
        public ActionResult Index()
        {
            return View(db.Estadisticas.ToList());
        }

        // GET: Estadisticas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadistica estadistica = db.Estadisticas.Find(id);
            if (estadistica == null)
            {
                return HttpNotFound();
            }
            return View(estadistica);
        }

        // GET: Estadisticas/Create
        public ActionResult Create()
        {
            ViewBag.Users = new SelectList(db.Users.Where(s => s.UserName != "Admin@gmail.com").Select(s => s.UserName).ToList());
            return View();
        }

        // POST: Estadisticas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstadisticaID,Date,TotalSpent")] Estadistica estadistica)
        {
            if (ModelState.IsValid)
            {
                var auxname = Request.Form["name"];
                var month = int.Parse(Request.Form["Month"]);
                var Socio = db.Users.Where(s => s.UserName.Equals(auxname)).FirstOrDefault();
                if (Socio != null)
                {
                    var suma = Socio.Alquileres.AsEnumerable().Where(s => (s.PickUpate.Month.Equals(month)) 
                                                                       && (s.DateOfReturn.Month.Equals(month) )).Sum(s => s.Cost);

                    estadistica.TotalSpent = suma;
                    db.Estadisticas.Add(estadistica);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(estadistica);
        }

        // GET: Estadisticas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadistica estadistica = db.Estadisticas.Find(id);
            if (estadistica == null)
            {
                return HttpNotFound();
            }
            return View(estadistica);
        }

        // POST: Estadisticas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstadisticaID,Date,TotalSpent")] Estadistica estadistica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadistica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadistica);
        }

        // GET: Estadisticas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadistica estadistica = db.Estadisticas.Find(id);
            if (estadistica == null)
            {
                return HttpNotFound();
            }
            return View(estadistica);
        }

        // POST: Estadisticas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estadistica estadistica = db.Estadisticas.Find(id);
            db.Estadisticas.Remove(estadistica);
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
