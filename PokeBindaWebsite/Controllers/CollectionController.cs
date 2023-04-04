using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokeBindaWebsite.Controllers
{
    public class CollectionController : Controller
    {
        // GET: Collection
        public ActionResult Index()
        {
            return View();
        }

        // GET: Group
        public ActionResult Group(int? group)
        {
            if (group == null)
            {
                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult NewGroup()
        {
            return View();
        }
    }
}