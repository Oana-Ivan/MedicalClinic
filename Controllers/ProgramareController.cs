using clinicaMedicala4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clinicaMedicala4.Controllers
{
    [Authorize(Roles = "Client, Admin")]
    public class ProgramareController : Controller
    {
        private DbCtx db = new DbCtx();
        // GET: Programare
        public ActionResult Index()
        {
            List<Programare> programari = db.Programari.ToList();
            ViewBag.Programari = programari;
            bool user = false;
            if (User.IsInRole("Admin") || User.IsInRole("Client"))
            {
                user = true;
            }
            ViewBag.user = user;
            return View();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ActionResult Detalii(int? id)
        {
            if (id.HasValue)
            {
                Programare aux = db.Programari.Find(id);

                bool admin = false;
                if (User.IsInRole("Admin") || User.IsInRole("Client"))
                {
                    admin = true;
                }
                ViewBag.admin = admin;

                if (aux != null)
                {
                    return View(aux);
                }
                return HttpNotFound("Nu s-a gasit programarea cu id-ul " + id.ToString());
            }
            return HttpNotFound("Lipseste id-ul programarii!");
        }

        //CRUD
        // 1. Create 
        [HttpGet]
        public ActionResult New()
        {
            Programare aux = new Programare();
            return View(aux);
        }

        [HttpPost]
        public ActionResult New(Programare ProgramareRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Programari.Add(ProgramareRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ProgramareRequest);
            }
            catch (Exception e)
            {
                return View(ProgramareRequest);
            }
        }

        // 2. Update
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Programare aux = db.Programari.Find(id);
                if (aux == null)
                {
                    return HttpNotFound("Nu s-a gasit programarea cu id-ul  " + id.ToString());
                }
                return View(aux);
            }
            return HttpNotFound("Lipseste parametrul corespunzator id-ului programarii!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Programare ProgramareRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Programare aux = db.Programari.SingleOrDefault(b => b.ProgramareId.Equals(id));
                    if (TryUpdateModel(aux))
                    {
                        aux.An = ProgramareRequest.An;
                        aux.Luna = ProgramareRequest.Luna;
                        aux.Zi = ProgramareRequest.Zi;
                        aux.Ora = ProgramareRequest.Ora;
                        aux.TipProgramare = ProgramareRequest.TipProgramare;
                        aux.Observatii = ProgramareRequest.Observatii;
                        aux.PacientId = ProgramareRequest.PacientId;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(ProgramareRequest);
            }
            catch (Exception e)
            {
                return View(ProgramareRequest);
            }
        }

        // 3. Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Programare aux = db.Programari.Find(id);
            if (aux != null)
            {
                db.Programari.Remove(aux);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Nu am gasit programarea cu id-ul  " + id.ToString());
        }
    }
}