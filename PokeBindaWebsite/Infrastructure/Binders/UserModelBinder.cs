using LogicLayer;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokeBindaWebsite.Infrastructure.Binders
{
    public class UserModelBinder : IModelBinder
    {
        private const string sessionKey = "User";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            DataObjects.User user = null;
            if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (controllerContext.HttpContext.Session != null)
                {
                    user = (DataObjects.User)controllerContext.HttpContext.Session[sessionKey];
                    if (user == null)
                    {
                        try
                        {
                            user = new UserManager(new CardManager()).LoginUser(controllerContext.HttpContext.User.Identity.GetUserName());
                            controllerContext.HttpContext.Session[sessionKey] = user;
                        }
                        catch 
                        {

                        }
                    }
                    else
                    {

                    }
                }
            }
            return user;
        }
    }
}