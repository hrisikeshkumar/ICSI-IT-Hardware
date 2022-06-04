using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;


namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class AppleIpadController : Controller
    {

        public ActionResult AppleIpad_Details()
        {
            BL_AppleIpad com = new BL_AppleIpad();

            List<Mod_AppleIpad> pc_List = com.Get_AppleIpadData();

            return View("~/Areas/Admin/Views/AppleIpad/AppleIpad_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Lap_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/AppleIpad/AppleIpad_Create_Item.cshtml");

        }

        [HttpPost]
        public ActionResult Lap_Create_Post(Mod_AppleIpad Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_AppleIpad save_data = new BL_AppleIpad();
                    int status = save_data.Save_AppleIpad_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "AppleIpad");
        }

        public ActionResult Edit_AppleIpad(string id)
        {
            BL_AppleIpad Md_Com = new BL_AppleIpad();
            Mod_AppleIpad data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/AppleIpad/Edit_AppleIpad.cshtml", data);
        }


        public ActionResult Update_AppleIpad(Mod_AppleIpad Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_AppleIpad Md_Asset = new BL_AppleIpad();

                    status = Md_Asset.Save_AppleIpad_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("AppleIpad_Details", "AppleIpad");
        }


        public ActionResult Delete_AppleIpad(Mod_AppleIpad Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_AppleIpad Md_Asset = new BL_AppleIpad();

                    status = Md_Asset.Save_AppleIpad_data(Get_Data, "Delete", id);

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

            return RedirectToAction("AppleIpad_Details", "AppleIpad");
        }


    }
}