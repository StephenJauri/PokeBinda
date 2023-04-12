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
        public async Task<ActionResult> Index(FilterOptionsModel filtering, User user)
        {
            var model = new CollectionModel()
            {
                Cards = user.PokemonCards
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", model);
            }
            return View("Index", model);
        }
         
        // GET: Group
        [HttpGet]
        public async Task<ActionResult> Group(int? group, FilterOptionsModel filtering, User user)
        {
            Debug.WriteLine(group);
            if (group == null)
            {
                Debug.WriteLine("Error");
                return await Index(filtering, user);
            }
            Debug.WriteLine(Request.IsAjaxRequest());
            var groups = user.Groups.ToList();
                groups.Add(user.FavoriteGroup);
            var model = new GroupModel()
            {
                CardGroup = groups.Find(gr => gr.ID == group),
                Options = null
            };
            if (model.CardGroup == null)
            {
                Response.StatusCode = 401;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Unauthorized");
                }
                return View("Unauthorized");
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("Group",model);
            }
            return View("Group", model);
        }

        public async Task<ActionResult> Card(int? card, User user)
        {
            var model = user.PokemonCards.Find(userCard => userCard.UserCardID == card);

            if (model == null)
            {
                Response.StatusCode = 401;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Unauthorized");
                }
                return View("Unauthorized");
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("ViewUserCard", model);
            }
            return View("ViewUserCard", model);
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