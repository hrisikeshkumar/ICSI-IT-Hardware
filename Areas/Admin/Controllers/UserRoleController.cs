using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;


namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class UserRoleController : Controller
    {

       // [Authorize(Roles = "SU, Admin")]
        public ActionResult UserRole_Details()
        {
            BL_UserRole com = new BL_UserRole();

            List<Mod_UserRole> Mod_userrole = com.Get_UserRoleData();

            return View("~/Areas/Admin/Views/UserRole/UserRole_Details.cshtml", Mod_userrole);
        }

        [HttpPost]
        //[Authorize(Roles = "SU")]
        public ActionResult Update_UserRole(Mod_UserRole[] Get_Data)
        {


            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_UserRole Md_Asset = new BL_UserRole();

                    status = Md_Asset.Save_UserRole_data(Get_Data, "Update");

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data saved successfully");
                    }
                    else
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = string.Format("ShowFailure();");
            }

            return RedirectToAction("UserRole_Details", "UserRole");
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