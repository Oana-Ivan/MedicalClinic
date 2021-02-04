using clinicaMedicala4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clinicaMedicala4.Controllers
{
    [AllowAnonymous]
    public class ClinicaMedicalaController : Controller
    {
        // GET: ClinicaMedicala
        //public ActionResult Index()
        //{
        //    return View();
        //}
        private ApplicationDbContext ctx = new ApplicationDbContext();

        public ActionResult PaginaPrincipala()
        {
            int rol = 0;

            if (User.IsInRole("Admin"))
            {
                rol = 1;
            }

            ViewBag.UsersNumber = ctx.Users.Count();
            ViewBag.rol = rol;

            return View();
        }
    }
}