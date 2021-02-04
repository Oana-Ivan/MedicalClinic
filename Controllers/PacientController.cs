using clinicaMedicala4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clinicaMedicala4.Controllers
{
    [Authorize(Roles = "Client, Admin")]
    public class PacientController : Controller
    {
        private DbCtx db = new DbCtx();
      
        // GET: Pacient
        public ActionResult Index()
        {
            List<Pacient> pacienti = db.Pacienti.ToList();
            ViewBag.Pacienti = pacienti;
            bool user = false;
            if (User.IsInRole("Admin") || User.IsInRole("Client"))
            {
                user = true;
            }
            ViewBag.user = user;
            return View();
        }

        [HttpGet]
        public ActionResult Detalii(int? id)
        {
            if (id.HasValue)
            {
                Pacient aux = db.Pacienti.Find(id);

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
                return HttpNotFound("Nu s-a gasit pacientul cu id-ul " + id.ToString());
            }
            return HttpNotFound("Lipseste id-ul pacientului!");
        }

        //CRUD
        // 1. Create 
        [HttpGet]
        public ActionResult New()
        {
            Pacient aux = new Pacient();
            //aux.ListaDoctori = GetAllDoctor();
            //aux.Doctori = new List<Doctor>();
            return View(aux);
        }

        [HttpPost]
        public ActionResult New(Pacient PacientRequest)
        {
            //var selectedDoctor = PacientRequest.ListaDoctori.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    //PacientRequest.Doctori = new List<Doctor>();
                    //for (int i = 0; i < selectedDoctor.Count(); i++)
                    //{
                    //    Doctor doc = db.Doctori.Find(selectedDoctor[i].Id);
                    //    PacientRequest.Doctori.Add(doc);
                    //}

                    db.Pacienti.Add(PacientRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(PacientRequest);
            }
            catch (Exception e)
            {
                return View(PacientRequest);
            }
        }

        // 2. Update
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Pacient aux = db.Pacienti.Find(id);
                //aux.ListaDoctori = GetAllDoctor();

                //foreach(Doctor checkedDoctor in aux.Doctori)
                //{
                //    aux.ListaDoctori.FirstOrDefault(d => d.Id == checkedDoctor.DoctorId).Checked = true;

                //}

                if (aux == null)
                {
                    return HttpNotFound("Nu s-a gasit pacientul cu id-ul  " + id.ToString());
                }
                return View(aux);
            }
            return HttpNotFound("Lipseste parametrul corespunzator id-ului pacientului!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Pacient PacientRequest)
        {
            Pacient aux = db.Pacienti.SingleOrDefault(b => b.PacientId.Equals(id));
            //var selectedDoctori = PacientRequest.ListaDoctori.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    // Pacient aux = db.Pacienti.SingleOrDefault(b => b.PacientId.Equals(id));
                    if (TryUpdateModel(aux))
                    {
                        aux.Nume = PacientRequest.Nume;
                        aux.Varsta = PacientRequest.Varsta;
                        aux.Diagnostic = PacientRequest.Diagnostic;
                        aux.Observatii = PacientRequest.Observatii;
                        aux.Istoric = PacientRequest.Istoric;

                        //aux.Doctori.Clear();
                        //aux.Doctori = new List<Doctor>();
                        //for (int i = 0; i < selectedDoctori.Count(); i++)
                        //{
                        //    Doctor doc = db.Doctori.Find(selectedDoctori[i].Id);
                        //    aux.Doctori.Add(doc);
                        //}

                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(PacientRequest);
            }
            catch (Exception e)
            {
                return View(PacientRequest);
            }
        }

        // 3. Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Pacient aux = db.Pacienti.Find(id);
            if (aux != null)
            {
                db.Pacienti.Remove(aux);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Nu am gasit pacientul cu id-ul  " + id.ToString());
        }

        //[NonAction]
        //public List<CheckBoxViewModel> GetAllDoctor()
        //{
        //    var checkboxList = new List<CheckBoxViewModel>();
        //    foreach (var doc in db.Doctori.ToList())
        //    {
        //        checkboxList.Add(new CheckBoxViewModel
        //        {
        //            Id = doc.DoctorId,
        //            Name = doc.Nume,
        //            Checked = false
        //        });
        //    }
        //    return checkboxList;
        //}
    }
}