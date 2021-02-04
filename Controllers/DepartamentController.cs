using clinicaMedicala4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clinicaMedicala4.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartamentController : Controller
    {
        private DbCtx db = new DbCtx();
    
        // GET: Departament
        public ActionResult Index()
        {
            List<Departament> dept = db.Departamente.ToList();
            ViewBag.Departamente = dept;

            //bool admin = false;
            //if (User.IsInRole("Admin"))
            //{
            //    admin = true;
            //}
            //ViewBag.admin = admin;
            return View();
        }

        [HttpGet]
        public ActionResult Detalii(int? id)
        {
            if (id.HasValue)
            {
                Departament dep = db.Departamente.Find(id);

                if (dep != null)
                {
                    return View(dep);
                }
                return HttpNotFound("Couldn't find the department id " + id.ToString());
            }
            return HttpNotFound("Missing department id!");
        }

        //CRUD
        // 1. Create 
        [HttpGet]
        public ActionResult New()
        {
            Departament dep = new Departament();
            return View(dep);
        }

        [HttpPost]
        public ActionResult New(Departament departamentRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Departamente.Add(departamentRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(departamentRequest);
            }
            catch (Exception e)
            {
                return View(departamentRequest);
            }
        }
        // 2. Update
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Departament dep = db.Departamente.Find(id);
                if (dep == null)
                {
                    return HttpNotFound("Nu s-a gasit departamentul cu id-ul  " + id.ToString());
                }
                return View(dep);
            }
            return HttpNotFound("Missing dep id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Departament departamentRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Departament dep = db.Departamente.SingleOrDefault(b => b.DepId.Equals(id));
                    if (TryUpdateModel(dep))
                    {
                        dep.Denumire = departamentRequest.Denumire;
                        dep.NrAngajati = departamentRequest.NrAngajati;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(departamentRequest);
            }
            catch (Exception e)
            {
                return View(departamentRequest);
            }
        }

        // 3. Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Departament dep = db.Departamente.Find(id);
            if (dep != null)
            {
                db.Departamente.Remove(dep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the book with id " + id.ToString());
        }
    }
}