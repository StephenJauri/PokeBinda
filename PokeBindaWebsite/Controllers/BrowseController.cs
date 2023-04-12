using DataObjects;
using LogicLayer;
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
    public class BrowseController : Controller
    {
        // GET: Browse
        public async Task<ActionResult> Index(FilterOptionsModel options)
        {
            List<DataObjects.PokemonCard> cards;
            try
            {
                cards =
                    await Task.Run(() =>
                    {
                        return CardManager.Instance.LoadAllActiveReleasedCards();
                    });
            }
            catch
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Error");
                }
                return View("Error");
            }
            var model = new BrowseModel()
            {
                Options = options,
                Cards = cards
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("BrowseCards", model);
            }
            return View("BrowseCards", model);
        }

        public async Task<ActionResult> Card(int? card)
        {
            if (!card.HasValue)
            {
                if (Request.IsAjaxRequest())
                { return PartialView("Error"); }
                return View("Error");
            }
            PokemonCard model;
            try
            {
                model = CardManager.Instance.LoadActiveReleasedCard(card.Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\n\n" + ex.InnerException.Message);
                model = null;
            }

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
                return PartialView("ViewCard", model);
            }
            return View("ViewCard", model);
        }

    }
}