using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.Models;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using System.Web.Mvc.Filters;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Budget_YearController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public ActionResult Budget_Year_Details()
        {
            BL_Budget_Year com = new BL_Budget_Year();
            Mod_Budget_Year mod_data = new Mod_Budget_Year();

            mod_data.List_Bud_Year=com.Get_Data();

            return View("~/Areas/Admin/Views/Budget_Year/Budget_Year_Details.cshtml", mod_data);

        }

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public ActionResult Budget_Year_Insert(Mod_Budget_Year data)
        {
           
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Budget_Year save_data = new BL_Budget_Year();
                    int status = save_data.Save_data(data, "Save");

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data saved successfully");
                    }
                    else
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                }
                else
                {
                    TempData["Message"] = String.Format("Required Data are not Provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Budget_Year_Details", "Budget_Year");
        }

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public ActionResult Budget_Year_Update(string Bud_Id)
        {
           
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Budget_Year save_data = new BL_Budget_Year();
                    Mod_Budget_Year data = new Mod_Budget_Year();
                    data.Bud_Id = Bud_Id;
                    int status = save_data.Save_data(data,"Delete");

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data saved successfully");
                    }
                    else
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                }
                else
                {
                    TempData["Message"] = String.Format("Required Data are not Provided");
                }

            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Budget_Year_Details", "Budget_Year");
        }

        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {

                if (filterContext.Result is HttpUnauthorizedResult)
                {
                    filterContext.Result = new RedirectResult("~/Authorization/AccessDedied");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Log_In/Log_In");
            }
        }

    }
}