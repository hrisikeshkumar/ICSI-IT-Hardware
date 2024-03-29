﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Budget_HeadController : Controller
    {
        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Budget_Head_Details()
        {
            BL_Budget_Head com = new BL_Budget_Head();

            List<Mod_Budget> pc_List = com.Get_BudgetData();

            return View("~/Areas/Admin/Views/Budget_Head/Budget_Head_Details.cshtml", pc_List);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult Budget_Head_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Budget Mod_data = new Mod_Budget();

            BL_Budget_Year bud_year = new BL_Budget_Year();

            Mod_data.Bud_year_List = bud_year.budget_year_dropdown();


            return View("~/Areas/Admin/Views/Budget_Head/Budget_Head_Create_Item.cshtml", Mod_data);
        }

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpPost]
        public ActionResult Budget_Head_CreateItem_Post(Mod_Budget Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_User = HttpContext.User.Identity.Name;

                if (ModelState.IsValid)
                {
                    BL_Budget_Head save_data = new BL_Budget_Head();
                    int status = save_data.Save_Budget_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Budget_Head_Create_Item", "Budget_Head");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Edit_Budget_Head(string id)
        {

            BL_Budget_Head BL_data = new BL_Budget_Head();
            Mod_Budget Model_data = new Mod_Budget();
           
            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            BL_Budget_Year bud_year = new BL_Budget_Year();

            Model_data.Bud_year_List = bud_year.budget_year_dropdown();


            return View("~/Areas/Admin/Views/Budget_Head/Edit_Budget_Head.cshtml", Model_data);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_Budget_Head(Mod_Budget Get_Data, string Budget_Head_Id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_User = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Budget_Head Md_Asset = new BL_Budget_Head();

                    status = Md_Asset.Save_Budget_data(Get_Data, "Update", Budget_Head_Id);

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

            return RedirectToAction("Budget_Head_Details", "Budget_Head");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Delete_Budget_Head(Mod_Budget Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Budget_Head Md_Asset = new BL_Budget_Head();

                    status = Md_Asset.Save_Budget_data(Get_Data, "Delete", id);

                    if (status < 1)
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

            return RedirectToAction("Budget_Head_Details", "Budget_Head");
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