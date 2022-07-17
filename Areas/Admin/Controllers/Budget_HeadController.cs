using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Budget_HeadController : Controller
    {

        public ActionResult Budget_Head_Details()
        {
            BL_Budget_Head com = new BL_Budget_Head();

            List<Mod_Budget> pc_List = com.Get_BudgetData();

            return View("~/Areas/Admin/Views/Budget_Head/Budget_Head_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Budget_Head_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Budget Mod_data = new Mod_Budget();
            
            return View("~/Areas/Admin/Views/Budget_Head/Budget_Head_Create_Item.cshtml", Mod_data);
        }

        [HttpPost]
        public ActionResult Budget_Head_CreateItem_Post(Mod_Budget Get_Data)
        {
            string Message = "";
            try
            {

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
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Budget_Create_Item", "Budget");
        }

        public ActionResult Edit_Budget_Head(string id)
        {

            BL_Budget_Head BL_data = new BL_Budget_Head();
            Mod_Budget Model_data = new Mod_Budget();
           
            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            return View("~/Areas/Admin/Views/Budget_Head/Edit_Budget_Head.cshtml", Model_data);
        }


        public ActionResult Update_Budget_Head(Mod_Budget Get_Data, string Budget_Head_Id)
        {
            int status = 0;
            try
            {

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
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Budget_Head_Details", "Budget_Head");
        }


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

    }

}