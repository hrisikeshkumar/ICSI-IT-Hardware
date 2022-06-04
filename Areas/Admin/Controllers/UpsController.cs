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
        public ActionResult Lap_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Ups/Ups_Create_Item.cshtml");

        }

        [HttpPost]
        public ActionResult Lap_Create_Post(Mod_Ups Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Ups save_data = new BL_Ups();
                    int status = save_data.Save_Ups_data(Get_Data, "Add_new", "");

                    if (status == 1)
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                    else
                    {

                        TempData["Message"] = String.Format("Data save successfully");
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Create_Item", "Ups");
        }

        public ActionResult Edit_Ups(string id)
        {
            BL_Ups Md_Com = new BL_Ups();
            Mod_Ups data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Ups/Edit_Ups.cshtml", data);
        }


        public ActionResult Update_Ups(Mod_Ups Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Ups Md_Asset = new BL_Ups();

                    status = Md_Asset.Save_Ups_data(Get_Data, "Update", Asset_ID);

                    if (status == 1)
                    {
                        TempData["Message"] = String.Format("Data have saved successfully");
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

    }
}