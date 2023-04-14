using DataObjects;
using LogicLayer;
using Microsoft.AspNet.Identity;
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
        [HttpGet]
        public async Task<ActionResult> Index(FilterOptionsModel options)
        {
            List<DataObjects.PokemonCard> cards;
            try
            {
                cards =
                    await Task.Run(() =>
                    {
                        return CardManager.Instance.LoadAllActiveReleasedCardsMinimum();
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

        [HttpGet]
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
                model = 
                await Task.Run(() =>
                {
                    return CardManager.Instance.LoadActiveReleasedCard(card.Value);
                });
            }
            catch (Exception ex)
            {
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Add(int? card, User user)
        {
            if (!card.HasValue)
            {
                if (Request.IsAjaxRequest())
                { return PartialView("Error"); }
                return View("Error");
            }
            UserPokemonCard newCard;
            try
            {
                newCard = 
                await Task.Run(() =>
                {
                    PokemonCard pokemonCard = CardManager.Instance.LoadActiveReleasedCard(card.Value);
                    var loadedCard = UserManager.Instance.CreateUserPokemonCard(pokemonCard);
                    UserManager.Instance.AddPokemonCardToCollection(loadedCard, user);
                    return loadedCard;
                });
            }
            catch (Exception ex)
            {
                newCard = null;
            }

            if (newCard == null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Error");
                }
                return View("Error");
            }

            return RedirectToAction("Card", "Collection", new {card = newCard.UserCardID});
        }
    }
}