using System;
using Microsoft.Owin;
using Owin;
using Mvc5SecurityProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(Mvc5SecurityProject.Startup))]
namespace Mvc5SecurityProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        // In this method we will create default user roles and admin user for login
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In startup, creating first Admin Role and creating a default Admin User
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin role
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                // Here we create an Admin Super User who will maintain the website

                var user = new ApplicationUser();
                user.UserName = "Sean";
                user.Email = "sean@example.com";

                string userPassword = "Sean@123";

                var checkUser = userManager.Create(user, userPassword);

                // Add default user to role admin
                if (checkUser.Succeeded)
                {
                    var checkResult = userManager.AddToRole(user.Id, "Admin");
                }                
            }

            // Creating manager role
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            // Creating employee role
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }
        }
    }
}
