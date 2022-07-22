using IT_Asset.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using IT_Hardware_Aug2021.User_Role;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Admin_DashboardController : Controller
    {
        // GET: Admin/Admin_Dashboard
        [Authorize(Roles ="SU")]
        public ActionResult Admin_Dashboard()
        {
            List<Admin_dashB> DashB = new List<Admin_dashB>();

            DashB.Add(new Admin_dashB { Item_Type = "Computer", Item_Sl_No = "rgr", Issued_To = "rgbgb", Move_From = "", Move_To = "", DateofMovement = DateTime.Now });
            DashB.Add(new Admin_dashB { Item_Type = "Laptop", Item_Sl_No = "grhryh", Issued_To = "wdca", Move_From = "", Move_To = "", DateofMovement = DateTime.Now });
            DashB.Add(new Admin_dashB { Item_Type = "Printer", Item_Sl_No = "54tg", Issued_To = "ukou", Move_From = "", Move_To = "", DateofMovement = DateTime.Now });
            DashB.Add(new Admin_dashB { Item_Type = "Scanner", Item_Sl_No = "46y7efg", Issued_To = "vcv v", Move_From = "", Move_To = "", DateofMovement = DateTime.Now });
            DashB.Add(new Admin_dashB { Item_Type = "Server", Item_Sl_No = "234fgf", Issued_To = "ljk,", Move_From = "", Move_To = "", DateofMovement = DateTime.Now });


            return View(DashB);
        }



        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Authorization/AccessDedied");
            }
        }

    }
}