using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Budget_UsesController : Controller
    {

        public ActionResult Budget_Uses_Details()
        {
            BL_Budget_Uses com = new BL_Budget_Uses();

            List<Mod_Budget> pc_List = com.Get_BudgetData();

            return View("~/Areas/Admin/Views/Budget_Uses/Budget_Uses_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Budget_Uses_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Budget Mod_data = new Mod_Budget();

            return View("~/Areas/Admin/Views/Budget_Uses/Budget_Uses_Create_Item.cshtml", Mod_data);
        }

        [HttpPost]
        public ActionResult Budget_Uses_CreateItem_Post(Mod_Budget Get_Data)
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

            return RedirectToAction("Budget_Create_Item", "Budget");
        }

        public ActionResult Edit_Budget_Uses(string id)
        {

            BL_Budget_Uses BL_data = new BL_Budget_Uses();
            Mod_Budget Model_data = new Mod_Budget();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            return View("~/Areas/Admin/Views/Budget_Uses/Edit_Budget_Uses.cshtml", Model_data);
        }


        public ActionResult Update_Budget_Uses(Mod_Budget Get_Data, string Budget_Uses_Id)
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


        public ActionResult Delete_Budget_Uses(Mod_Budget Get_Data, string id)
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

    }

}