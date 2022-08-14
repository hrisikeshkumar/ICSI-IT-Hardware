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
    public class Budget_UsesController : Controller
    {
        //[Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public ActionResult Budget_Uses_Details()
        {
            BL_Budget_Uses com = new BL_Budget_Uses();

            Mod_Budget_Uses Mod_Budget_Uses = new Mod_Budget_Uses();

             Mod_Budget_Uses.Bud_us_list = com.Get_BudgetData();

            BL_Budget_Year b_year = new BL_Budget_Year();
            Mod_Budget_Uses.Budget_Year_List = b_year.budget_year_dropdown();

            return View("~/Areas/Admin/Views/Budget_Uses/Budget_Uses_Details.cshtml", Mod_Budget_Uses);
        }

        //[Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult Budget_Uses_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Budget_Uses Mod_data = new Mod_Budget_Uses();

            BL_Budget_Year bud_year = new BL_Budget_Year();

            Mod_data.Budget_Year_List = bud_year.budget_year_dropdown();

            return View("~/Areas/Admin/Views/Budget_Uses/Budget_Uses_Create_Item.cshtml", Mod_data);
        }

        //[Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        [HttpPost]
        public ActionResult Budget_Uses_CreateItem_Post(Mod_Budget_Uses Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Budget_Uses save_data = new BL_Budget_Uses();
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
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Budget_Uses_Details", "Budget_Uses");
        }

        //[Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public ActionResult Edit_Budget_Uses(string id)
        {

            BL_Budget_Uses BL_data = new BL_Budget_Uses();
            Mod_Budget_Uses Model_data = new Mod_Budget_Uses();

           

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            BL_Budget_Year bud_year = new BL_Budget_Year();

            Model_data.Budget_Year_List = bud_year.budget_year_dropdown();

            BL_data.Get_Budget_Head(Model_data, Model_data.Budget_Year);


            return View("~/Areas/Admin/Views/Budget_Uses/Edit_Budget_Uses.cshtml", Model_data);
        }

        //[Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public ActionResult Update_Budget_Uses(Mod_Budget_Uses Get_Data, string Budget_Uses_Id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Budget_Uses Md_Asset = new BL_Budget_Uses();

                    status = Md_Asset.Save_Budget_data(Get_Data, "Update", Budget_Uses_Id);

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

            return RedirectToAction("Budget_Uses_Details", "Budget_Uses");
        }

        //[Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public ActionResult Delete_Budget_Uses(Mod_Budget_Uses Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {

                    BL_Budget_Uses Md_Asset = new BL_Budget_Uses();

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

            return RedirectToAction("Budget_Uses_Details", "Budget_Uses");
        }


        //[Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public JsonResult Budget_List(string Yearcode)
        {

            BL_Budget_Uses Bud_List = new BL_Budget_Uses();

            Mod_Budget_Uses Mod_budget = new Mod_Budget_Uses();

            Bud_List.Get_Budget_Head(Mod_budget, Yearcode);

            return Json(Mod_budget.Budget_List , JsonRequestBehavior.AllowGet);

        }


        //[Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public JsonResult Prev_Budget_info(string Bud_Head_Id, string Yearcode)
        {

            BL_Budget_Uses Bud_List = new BL_Budget_Uses();

            Mod_Budget_Uses Mod_budget = new Mod_Budget_Uses();

            Bud_List.Get_Prev_Budget_Uses(Mod_budget, Bud_Head_Id,  Yearcode);

            return Json(Mod_budget, JsonRequestBehavior.AllowGet);

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



        public JsonResult Budget_Uses_List(string Bud_head_Id)
        {

            BL_Budget_Uses Bud_List = new BL_Budget_Uses();

            return Json(Bud_List.Get_BudgetUses_By_BudId(Bud_head_Id), JsonRequestBehavior.AllowGet);

        }



    }

}