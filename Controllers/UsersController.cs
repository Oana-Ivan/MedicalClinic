using clinicaMedicala4.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clinicaMedicala4.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            ViewBag.UsersList = ctx.Users.OrderBy(u => u.UserName).ToList();
            return View();
        }

        public ActionResult Detalii(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return HttpNotFound("Lipseste id-ul!");
            }

            ApplicationUser user = ctx.Users.Include("Roles").FirstOrDefault(u => u.Id.Equals(id));

            if (user != null)
            {
                ViewBag.UserRole = ctx.Roles.Find(user.Roles.First().RoleId).Name;
                return View(user);
            }
            return HttpNotFound("Nu s-a gasit user-ul cu id-ul dat!");
        }

        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return HttpNotFound("Lipseste id-ul!");
            }

            UserViewModel uvm = new UserViewModel();
            uvm.User = ctx.Users.Find(id);

            IdentityRole userRole = ctx.Roles.Find(uvm.User.Roles.First().RoleId);
            uvm.RoleName = userRole.Name;

            return View(uvm);
        }

        [HttpPost]
        public ActionResult Edit(string id, UserViewModel uvm)
        {
            ApplicationUser user = ctx.Users.Find(id);
            try
            {
                if (TryUpdateModel(user))
                {
                    var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

                    Console.WriteLine("site-ul asta e naspa");
                    foreach (var r in ctx.Roles.ToList())
                    {
                        um.RemoveFromRole(user.Id, r.Name);
                    }
                    um.AddToRole(user.Id, uvm.RoleName);
                    user.UserName = uvm.User.Email;
                    user.Email = uvm.User.Email;
                    ctx.SaveChanges();
                    Console.WriteLine("well, macar a ajuns aici");
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(uvm);
            }
        }

    }
}