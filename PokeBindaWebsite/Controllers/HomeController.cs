﻿using LogicLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PokeBindaWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokeBindaWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && Session["User"] == null)
            {
                Session["User"] = new UserManager(new CardManager()).LoginUser(User.Identity.GetUserName());
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}