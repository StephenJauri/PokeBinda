using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayerInterfaces;
using LogicLayer;
using DataObjects;

namespace PokeBindaOnline.Controllers
{
    public class CollectionController : Controller
    {
        private IUserManager _userManager = new UserManager();


        [Authorize]
        public ActionResult Index()
        {
            List<UserPokemonCard> userCards = null;
            //try
            //{
            //    userCards = _userManager.
            //}
            return View();
        }
    }
}