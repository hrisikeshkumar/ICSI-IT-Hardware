using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class VendorController : Controller
    {

        public ActionResult Vendor_Details()
        {
            BL_Vendor com = new BL_Vendor();

            List<Mod_Vendor> pc_List = com.Get_VendorData();

            return View("~/Areas/Admin/Views/Vendor/Vendor_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Vendor_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Vendor/Vendor_Create_Item.cshtml");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vendor_Create_Post(Mod_Vendor Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Vendor save_data = new BL_Vendor();
                    int status = save_data.Save_Vendor_data(Get_Data, "Add_new", "");

                    if (status > 0)
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

            return RedirectToAction("Vendor_Create_Item", "Vendor");
        }

        public ActionResult Edit_Vendor(string id)
        {
            BL_Vendor Md_Com = new BL_Vendor();
            Mod_Vendor data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Vendor/Edit_Vendor.cshtml", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update_Vendor(Mod_Vendor Get_Data)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Vendor Md_Asset = new BL_Vendor();

                    status = Md_Asset.Save_Vendor_data(Get_Data, "Update", Get_Data.Vendor_id);

                    if (status > 0)
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

            return RedirectToAction("Vendor_Details", "Vendor");
        }


        public ActionResult Delete_Vendor(Mod_Vendor Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Vendor Md_Asset = new BL_Vendor();

                    status = Md_Asset.Save_Vendor_data(Get_Data, "Delete", id);

                    if (status >0)
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

            return RedirectToAction("Vendor_Details", "Vendor");
        }


    }
}