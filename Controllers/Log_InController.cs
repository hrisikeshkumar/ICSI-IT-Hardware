﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IT_Hardware_Aug2021.Models;
using IT_Hardware_Aug2021.BAL;

namespace IT_Asset.Controllers
{
    public class Log_InController : Controller
    {
        // GET: Log_In
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Log_In()
        {
            Mod_LogIn Model = new Mod_LogIn();

            return View("Log_In", Model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Log_In(Mod_LogIn model)
        {

            BL_LogIn Bal = new BL_LogIn();

            if (Bal.User_Authentication(model))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                Session["Username"] = Convert.ToString(model.UserName);
                return RedirectToAction("Admin_Dashboard", "Admin_Dashboard" , new { area = "Admin" });
            }

            ModelState.AddModelError("", "invalid Username or Password");

            return View("Log_In");
                 
        }

        [HttpGet]
        public ActionResult Log_Out()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Log_In");

        }
    }
}


