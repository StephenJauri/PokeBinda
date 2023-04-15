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
                Cards = user.PokemonCards,
                Options = filtering
            };
            try
            {
                await Task.Run(() =>
                {
                    model.Options.Tags = LogicLayer.LookupManager.Instance.GetAllTags();
                    model.Options.Types = LogicLayer.LookupManager.Instance.GetAllTypes();
                    model.Options.SelectablePokemon = LogicLayer.LookupManager.Instance.GetAllPokemon();
                });
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Index", model);
                }
                return View("Index", model);
            }
            catch
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Error");
                }
                return View("Error");
            }
        }
         
        // GET: Group
        [HttpGet]
        public async Task<ActionResult> Group(int? group, FilterOptionsModel filtering, User user)
        {
            if (group == null)
            {
                return await Index(filtering, user);
            }
            var groups = user.Groups.ToList();
                groups.Add(user.FavoriteGroup);

            var userCardGroup = groups.Find(gr => gr.ID == group);
            if (userCardGroup == null)
            {
                Response.StatusCode = 401;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Unauthorized");
                }
                return View("Unauthorized");
            }
            else
            {

                var cards = userCardGroup.Cards;
                if (filtering.Tag != null)
                {
                    cards = cards.Where(c => c.Tags.Contains(filtering.Tag)).ToList();
                }
                if (filtering.Type != null)
                {
                    cards = cards.Where(c => c.Types.Contains(filtering.Type)).ToList();
                }
                if (filtering.Pokemon != null)
                {
                    cards = cards.Where(c => c.Pokemon.Exists(p => p.ID == filtering.Pokemon)).ToList();
                }
                var model = new GroupModel()
                {
                    Cards = cards,
                    Group = userCardGroup.ID,
                    Options = filtering
                };
                try
                {
                    await Task.Run(() =>
                    {
                        model.Options.Tags = LogicLayer.LookupManager.Instance.GetAllTags();
                        model.Options.Types = LogicLayer.LookupManager.Instance.GetAllTypes();
                        model.Options.SelectablePokemon = LogicLayer.LookupManager.Instance.GetAllPokemon();
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
                if (Request.IsAjaxRequest())
            {
                return PartialView("Group",model);
            }
            return View("Group", model);
            }
        }

        public async Task<ActionResult> Card(int? card, User user)
        {
            var model = new ViewUserCardModel();
            model.Card = user.PokemonCards.Find(userCard => userCard.UserCardID == card);
            try
            {
                model.Statuses =
                    await Task.Run(() => LogicLayer.LookupManager.Instance.GetAllStatuses());

                var allGroups = user.Groups.ToList();
                allGroups.Insert(0, user.FavoriteGroup);

                var removeGroups = model.Card.Groups.ToList();
                var addGroups = allGroups.Except(removeGroups).ToList();
                model.RemoveGroups = removeGroups;
                model.AddGroups = addGroups;
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Error");
                }
                return View("Error");
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

            model.IsFavorite = user.FavoriteGroup.Cards.Contains(model.Card);
            if (Request.IsAjaxRequest())
            {
                return PartialView("ViewUserCard", model);
            }
            return View("ViewUserCard", model);
        }

        [HttpGet]
        public async Task<ActionResult> NewGroup()
        {
            if (User.IsInRole("Premium"))
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView();
                }
                return View();
            }
            else // change later to redirect to a upgrade to premium page
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView();
                }
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewGroup(NewGroupModel model, User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    LogicLayer.UserManager.Instance.CreateGroup(user, model.Name);
                    if (Request.IsAjaxRequest())
                    {
                        return RedirectToAction("Navigation", "Home");
                    }
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    if (Request.IsAjaxRequest())
                    {
                        return RedirectToAction("Navigation", "Home");
                    }
                    return View("Error");
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return RedirectToAction("Navigation", "Home");
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateStatus(CardStatusModel model, User user)
        {
            await Task.Delay(200);
            try
            {
                if (model.Card == null)
                {
                    throw new ApplicationException("No card to update");
                }
                if (model.SelectedStatus == null || model.SelectedStatus == "")
                {
                    throw new ApplicationException("Select a Status");
                }
                UserPokemonCard card = 
                await Task.Run(() =>
                {
                    UserPokemonCard loadedCard = user.PokemonCards.Find(userCard => userCard.UserCardID == model.Card.Value); ;
                    LogicLayer.UserManager.Instance.UpdateUserPokemonCardStatus(loadedCard, model.SelectedStatus, false);
                    model.Statuses = LogicLayer.LookupManager.Instance.GetAllStatuses();
                    return loadedCard;
                });
                if (Request.IsAjaxRequest())
                {
                    return Content(card.Status);
                }
                return RedirectToAction("Card", new { card = model.Card });
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                {
                    Response.StatusCode = 406;
                    return Content(ex.Message);
                }
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ToggleFavorite(int? card, User user)
        {
            bool isFavorite = false;
            try
            {
                if (card == null || !user.PokemonCards.Exists(c => c.UserCardID == card.Value))
                {
                    Response.StatusCode = 406;
                    throw new ApplicationException("No such card");
                }
                var userCard = user.PokemonCards.Find(c => c.UserCardID == card.Value);
                isFavorite = user.FavoriteGroup.Cards.Contains(userCard);
                if (isFavorite)
                {
                    LogicLayer.UserManager.Instance.RemoveCardFromGroup(userCard, user.FavoriteGroup);
                }
                else
                {
                    LogicLayer.UserManager.Instance.AddCardToGroup(userCard, user.FavoriteGroup);
                }

                isFavorite = !isFavorite;
                if (Request.IsAjaxRequest())
                {

                    var allGroups = user.Groups.ToList();
                    allGroups.Insert(0, user.FavoriteGroup);

                    var removeGroups = userCard.Groups.Select(gr => new { name = gr.Name, id = gr.ID }).ToList();
                    var addGroups = allGroups.Except(userCard.Groups).Select(gr => new { name = gr.Name, id = gr.ID }).ToList();
                    return Json(new
                    {
                        changedGroup = user.FavoriteGroup.ID,
                        isFavorite = isFavorite,
                        newAddGroups = addGroups,
                        newRemoveGroups = removeGroups
                    });
                }
                return RedirectToAction("Card", new { card = card });
            }
            catch
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new { isFavorite = isFavorite});
                }
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCardToGroup(int? card, int? group, User user)
        {
            try
            {
                if (!card.HasValue || !card.HasValue)
                {
                    throw new ApplicationException("Invalid card or group");
                }
                var userCard = user.PokemonCards.Find(c => c.UserCardID == card.Value);
                var allGroups = user.Groups.ToList();
                allGroups.Insert(0,user.FavoriteGroup);
                var userGroup = allGroups.Find(g => g.ID == group.Value);
                if (userCard == null || userGroup == null)
                {
                    throw new ApplicationException("Cannot access card or group");
                }
                LogicLayer.UserManager.Instance.AddCardToGroup(userCard, userGroup);
                if (Request.IsAjaxRequest())
                {
                    var removeGroups = userCard.Groups.Select(gr => new { name = gr.Name, id = gr.ID }).ToList();
                    var addGroups = allGroups.Except(userCard.Groups).Select(gr => new { name = gr.Name, id = gr.ID }).ToList();

                    return Json(new
                    {
                        changedGroup = userGroup.ID,
                        isFavorite = user.FavoriteGroup.Cards.Exists(c => c.UserCardID == userCard.UserCardID),
                        newAddGroups = addGroups,
                        newRemoveGroups = removeGroups
                    });
                }
                return RedirectToAction("Card", new { card = userCard.UserCardID });
            }
            catch (Exception ex)
            {
                if (Request.IsAuthenticated)
                {
                    Response.StatusCode = 406;
                    return Content(ex.Message);
                }
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveCardFromGroup(int? card, int? group, User user)
        {
            try
            {
                if (!card.HasValue || !card.HasValue)
                {
                    throw new ApplicationException("Invalid card or group");
                }
                var userCard = user.PokemonCards.Find(c => c.UserCardID == card.Value);
                var allGroups = user.Groups.ToList();
                allGroups.Insert(0, user.FavoriteGroup);
                var userGroup = allGroups.Find(g => g.ID == group.Value);
                if (userCard == null || userGroup == null)
                {
                    throw new ApplicationException("Cannot access card or group");
                }
                LogicLayer.UserManager.Instance.RemoveCardFromGroup(userCard, userGroup);
                if (Request.IsAjaxRequest())
                {
                    var removeGroups = userCard.Groups.Select(gr => new { name = gr.Name, id = gr.ID }).ToList();
                    var addGroups = allGroups.Except(userCard.Groups).Select(gr => new { name = gr.Name, id = gr.ID }).ToList();

                    return Json(new
                    {
                        changedGroup = userGroup.ID,
                        isFavorite = user.FavoriteGroup.Cards.Exists(c => c.UserCardID == userCard.UserCardID),
                        newAddGroups = addGroups,
                        newRemoveGroups  = removeGroups
                    });
                }
                return RedirectToAction("Card", new { card = userCard.UserCardID });
            }
            catch (Exception ex)
            {
                if (Request.IsAuthenticated)
                {
                    Response.StatusCode = 406;
                    return Content(ex.Message);
                }
                return View("Error");
            }
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> RemoveCard(int? card, User user)
        {
            if (!card.HasValue)
            {
                if (Request.IsAjaxRequest())
                { return PartialView("Error"); }
                return View("Error");
            }
            try
            {
                var userCard = user.PokemonCards.First(c => c.UserCardID == card.Value);
                await Task.Run(() =>
                {
                    LogicLayer.UserManager.Instance.DeleteUserPokemonCard(userCard, user);
                });
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true });
                }
                return RedirectToAction("Index", "Collection");
            }
            catch (Exception ex)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = false });
                }
                return View("Error");
            }
        }
    }
}