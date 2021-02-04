using clinicaMedicala4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clinicaMedicala4.Controllers
{
    public class FilialaController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Filiala
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Filiala> filiale = db.Filiale.Include("Departamente").ToList();
            ViewBag.Filiale = filiale;
            int rol = 0;

            if (User.IsInRole("Admin"))
            {
                rol = 1;
            }
            ViewBag.rol = rol;

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Detalii(int? id)
        {
            if (id.HasValue)
            {
                Filiala f = db.Filiale.Find(id);
                int rol = 0;

                if (User.IsInRole("Admin"))
                {
                    rol = 1;
                }
                ViewBag.rol = rol;

                if (f != null)
                {
                    return View(f);
                }
                return HttpNotFound("Nu s-a gasit filiala cu id-ul " + id.ToString());
            }
            return HttpNotFound("Lipseste id-ul filialei!");
        }

        //CRUD
        // 1. Create 
        [HttpGet]
        public ActionResult New()
        {
            Filiala f = new Filiala();
            //f.Programari = new List<Programare>();
            //f.Departamente = new List<Departament>();
            return View(f);
        }

        [HttpPost]
        public ActionResult New(Filiala filialaRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // filialaRequest.Departamente = db.Departamente.FirstOrDefault(p => p.DepId.Equals(1));

                    db.Filiale.Add(filialaRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(filialaRequest);
            }
            catch (Exception e)
            {
                return View(filialaRequest);
            }
        }

        // 2. Update
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Filiala f = db.Filiale.Find(id);
                if (f == null)
                {
                    return HttpNotFound("Nu s-a gasit filiala cu id-ul  " + id.ToString());
                }
                return View(f);
            }
            return HttpNotFound("Lipseste parametrul corespunzator id-ului filialei!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Filiala filialaRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Filiala f = db.Filiale.Include("Departament").SingleOrDefault(b => b.DepartamentId.Equals(id));
                    Filiala f = db.Filiale.SingleOrDefault(b => b.FilialaId.Equals(id));
                    if (TryUpdateModel(f))
                    {
                        f.Denumire = filialaRequest.Denumire;
                        f.Adresa = filialaRequest.Adresa;
                        f.Sediu = filialaRequest.Sediu;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(filialaRequest);
            }
            catch (Exception e)
            {
                return View(filialaRequest);
            }
        }

        // 3. Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Filiala f = db.Filiale.Find(id);
            if (f != null)
            {
                db.Filiale.Remove(f);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Nu am gasit filiala cu id-ul  " + id.ToString());
        }

            //[NonAction]
            //public IEnumerable<SelectListItem> GetAllBookTypes()
            //{
            //    var selectList = new List<SelectListItem>();

            //    foreach (var cover in db.BookTypes.ToList())
            //    {
            //        selectList.Add(new SelectListItem
            //        {
            //            Value = cover.BookTypeId.ToString(),
            //            Text = cover.Name
            //        });
            //    }
            //    return selectList;
            //}
        }
    }