using clinicaMedicala4.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(clinicaMedicala4.Startup))]
namespace clinicaMedicala4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdminAndUserRoles();
        }

        private void CreateAdminAndUserRoles()
        {
            var ctx = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";

                var adminCreated = userManager.Create(user, "parolaSigura");
                if (adminCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }

            }

            if (!roleManager.RoleExists("Client"))
            {
                var role = new IdentityRole();
                role.Name = "Client";
                roleManager.Create(role);
                // var user = new ApplicationUser();
                // user.UserName = "pacient";
                // user.Email = "pacient@gmail.com";
            }

            //if (!roleManager.RoleExists("Doctor"))
            //{
            //    var role = new IdentityRole();
            //    role.Name = "Doctor";
            //    roleManager.Create(role);

            //    // var user = new ApplicationUser();
            //    // user.UserName = "pacient";
            //    // user.Email = "pacient@gmail.com";
            //}
        }
    }
}
