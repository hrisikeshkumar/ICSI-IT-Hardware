using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class UpsController : Controller
    {


        public ActionResult Ups_Details()
        {
            BL_Ups com = new BL_Ups();

            List<Mod_Ups> pc_List = com.Get_UpsData();

            return View("~/Areas/Admin/Views/Ups/Ups_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Ups_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Ups Mod_data = new Mod_Ups();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("UPS", "MAKE", "");

            return View("~/Areas/Admin/Views/Ups/Ups_Create_Item.cshtml", Mod_data);

        }

        [HttpPost]
        public ActionResult Ups_CreateItem_Post(Mod_Ups Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Ups save_data = new BL_Ups();
                    int status = save_data.Save_Ups_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Ups_Create_Item", "Ups");
        }

        public ActionResult Edit_Ups(string id)
        {
           
            BL_Ups BL_data = new BL_Ups();
            Mod_Ups Model_data = new Mod_Ups();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("UPS", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("UPS", "MODEL", Model_data.Item_Make_id.Trim().ToString());

            return View("~/Areas/Admin/Views/Ups/Edit_Ups.cshtml", Model_data);
        }


        public ActionResult Update_Ups(Mod_Ups Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Ups Md_Asset = new BL_Ups();

                    status = Md_Asset.Save_Ups_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("Ups_Details", "Ups");
        }


        public ActionResult Delete_Ups(Mod_Ups Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Ups Md_Asset = new BL_Ups();

                    status = Md_Asset.Save_Ups_data(Get_Data, "Delete", id);

                    if (status == 1)
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

            return RedirectToAction("Ups_Details", "Ups");
        }


        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("UPS", "MODEL", Item_Make);

            return Json(Mod_Make.Item_Model_List, JsonRequestBehavior.AllowGet);

        }


    }
}