using IT_Asset.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IT_Hardware_Aug2021.User_Role;
using System.Web.Mvc.Filters;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Admin_DashboardController : Controller
    {

        //[Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Admin_Dashboard()
        {

            BL_Admin_DashB B_Layer = new BL_Admin_DashB();

            Mod_Admin_dashB mod_Data = new Mod_Admin_dashB();

            mod_Data.List_Proposal = B_Layer.Get_List_Proposal();


            return View("~/Areas/Admin/Views/Admin_Dashboard/Admin_Dashboard.cshtml", mod_Data);

           
        }


        //protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        //{
        //    if (filterContext.HttpContext.Request.IsAuthenticated)
        //    {

        //        if (filterContext.Result is HttpUnauthorizedResult)
        //        {
        //            filterContext.Result = new RedirectResult("~/Authorization/AccessDedied");
        //        }
        //    }
        //    else
        //    {
        //        filterContext.Result = new RedirectResult("~/Log_In/Log_In");
        //    }
        //}

    }
}