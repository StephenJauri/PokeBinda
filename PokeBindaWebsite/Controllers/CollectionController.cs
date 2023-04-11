using DataObjects;
using PokeBindaWebsite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PokeBindaWebsite.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        // GET: Collection
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index");
            }
            return View("Index");
        }
         
        // GET: Group
        [HttpGet]
        public async Task<ActionResult> Group(int? group, User user)
        {
            await Task.Delay(100);
            Debug.WriteLine(group);

            if (group == null)
            {
                Debug.WriteLine("Error");
                return Index();
            }
            Debug.WriteLine(Request.IsAjaxRequest());
            var groups = user.Groups.ToList();
                groups.Add(user.FavoriteGroup);
            var model = new GroupModel()
            {
                CardGroup = groups.First(gr => gr.ID == group),
                Options = null
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("Group",model);
            }
            return View("Group", model);
        }

        public ActionResult NewGroup()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }
    }
}