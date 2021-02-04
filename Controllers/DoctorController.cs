using clinicaMedicala4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clinicaMedicala4.Controllers
{
    public class DoctorController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Doctor
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Doctor> doctori = db.Doctori.ToList();
            ViewBag.Doctori = doctori;
            bool admin = false;
            if (User.IsInRole("Admin"))
            {
                admin = true;
            }
            ViewBag.admin = admin;
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Detalii(int? id)
        {
            if (id.HasValue)
            {
                Doctor aux = db.Doctori.Find(id);

                bool admin = false;
                if (User.IsInRole("Admin"))
                {
                    admin = true;
                }
                ViewBag.admin = admin;

                if (aux != null)
                {
                    return View(aux);
                }
                return HttpNotFound("Nu s-a gasit doctorul cu id-ul " + id.ToString());
            }
            return HttpNotFound("Lipseste id-ul doctorului!");
        }

        //CRUD
        // 1. Create 
        [HttpGet]
        public ActionResult New()
        {
            Doctor aux = new Doctor();
            return View(aux);
        }

        [HttpPost]
        public ActionResult New(Doctor doctorRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Doctori.Add(doctorRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(doctorRequest);
            }
            catch (Exception e)
            {
                return View(doctorRequest);
            }
        }

        // 2. Update
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Doctor aux = db.Doctori.Find(id);
                if (aux == null)
                {
                    return HttpNotFound("Nu s-a gasit doctorul cu id-ul  " + id.ToString());
                }
                return View(aux);
            }
            return HttpNotFound("Lipseste parametrul corespunzator id-ului doctorului!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Doctor doctorRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Doctor aux = db.Doctori.SingleOrDefault(b => b.DoctorId.Equals(id));
                    if (TryUpdateModel(aux))
                    {
                        aux.Nume = doctorRequest.Nume;
                        aux.Specializare = doctorRequest.Specializare;
                        aux.Varsta = doctorRequest.Varsta;
                        aux.Adresa = doctorRequest.Adresa;
                        aux.NrTelefon = doctorRequest.NrTelefon;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(doctorRequest);
            }
            catch (Exception e)
            {
                return View(doctorRequest);
            }
        }

        // 3. Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Doctor aux = db.Doctori.Find(id);
            if (aux != null)
            {
                db.Doctori.Remove(aux);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Nu am gasit doctorul cu id-ul  " + id.ToString());
        }
    }
}