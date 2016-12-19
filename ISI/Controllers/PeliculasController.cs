using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ISI.Models;
using Microsoft.AspNet.Identity;

namespace ISI.Controllers
{
    public class PeliculasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Peliculas
        public ActionResult Index()
        {
            return View(db.Peliculas.ToList());
        }

        // GET: Peliculas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // GET: Peliculas/Create
        public ActionResult Create()
        {
            ViewBag.name = new SelectList(db.VideoClubs.Select(s => s.Name).ToList());
            return View();
        }

        // POST: Peliculas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PeliculaID,Name,Director,ReleaseDate,Price")] Pelicula pelicula)
        {
            try
            {
                List<VideoClub> listaVC = new List<VideoClub>();
                VideoClub vc = null;
                foreach (VideoClub vd in db.VideoClubs)
                {
                    listaVC.Add(vd);
                }

                if (listaVC != null)
                {
                    string[] strCadena = Request.Form["name"].Split(',');
                    var strnombre = strCadena[1];
                    vc = listaVC.Where(s => s.Name.Equals(strnombre)).FirstOrDefault();
                }

                if (vc != null)
                    pelicula.VideoClub = vc;
            }
            catch { }

            if (ModelState.IsValid)
            {
                db.Peliculas.Add(pelicula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PeliculaID,Name,Director,ReleaseDate,Price")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pelicula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pelicula pelicula = db.Peliculas.Find(id);
            db.Peliculas.Remove(pelicula);
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


        /***
         * 
         * Metodos añadidos
         * 
         * */

        // GET: Peliculas/Alquilar
        public ActionResult Alquilar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // POST: Peliculas/Alquilar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alquilar(FormCollection values)
        {
            if (values == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                int id = int.Parse(values["PeliculaID"]);
                Pelicula pelicula = db.Peliculas.Find(id);
                Alquiler auxAlquiler = new Alquiler();
                var Socio = db.Users.Find(User.Identity.GetUserId());

                auxAlquiler.PickUpate = DateTime.Parse(values["Alquiler.PickUpate"]);
                auxAlquiler.DateOfReturn = DateTime.Parse(values["Alquiler.DateOfReturn"]);
                auxAlquiler.Cost = pelicula.Price;
                auxAlquiler.Socio = Socio;

                Socio.Alquileres.Add(auxAlquiler);
                pelicula.Alquiler = auxAlquiler;

                auxAlquiler.Peliculas.Add(pelicula);
                db.Alquilers.Add(auxAlquiler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}
