using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PokeBindaWebsite.Models;

namespace PokeBindaWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager userManager;

        // GET: Admin
        public ActionResult Index()
        {
            //return View(db.ApplicationUsers.ToList());
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(userManager.Users.OrderBy(n => n.FamilyName).ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var employeeManager = new LogicLayer.EmployeeManager();
            var allRoles = employeeManager.GetAllRoles();

            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View(applicationUser);
        }

        public ActionResult RemoveRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            if (role == "Admin")
            {
                var adminUsers = userManager.Users
                    .ToList()
                    .Where(u => userManager.IsInRole(u.Id, "Admin"))
                    .ToList()
                    .Count();
                if (adminUsers < 2)
                {
                    ViewBag.Error = "Cannot remove last administrator.";
                    return RedirectToAction("Details", "Admin", new { id = user.Id });
                }
            }
            userManager.RemoveFromRole(id, role);

            if (user.EmployeeID != null)
            {
                try
                {
                    var employeeManager = new LogicLayer.EmployeeManager();
                    employeeManager.DeleteEmployeeRole(user.EmployeeID.Value, role);
                }
                catch(Exception ex)
                {
                    ViewBag.Error = ex.Message + "\n\n" + ex.InnerException.Message;
                    return View("Error");
                }
            }

            return RedirectToAction("Details", "Admin", new { id = user.Id});
        }

        public ActionResult AddRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            userManager.AddToRole(id, role);

            if(user.EmployeeID != null)
            {
                try
                {
                    var employeeManager = new LogicLayer.EmployeeManager();
                    employeeManager.AddEmployeeRole(user.EmployeeID.Value, role);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message + "\n\n" + ex.InnerException.Message;
                    return View("Error");
                }
            }

            return RedirectToAction("Details", "Admin", new { id = user.Id});
        }
    }
}
