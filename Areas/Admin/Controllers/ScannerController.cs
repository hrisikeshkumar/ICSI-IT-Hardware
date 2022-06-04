using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class ScannerController : Controller
    {

        public ActionResult Scanner_Details()
        {
            BL_Scanner com = new BL_Scanner();

            List<Mod_Scanner> pc_List = com.Get_ScannerData();

            return View("~/Areas/Admin/Views/Scanner/Scanner_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Lap_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Scanner/Scanner_Create_Item.cshtml");

        }

        [HttpPost]
        public ActionResult Lap_Create_Post(Mod_Scanner Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Scanner save_data = new BL_Scanner();
                    int status = save_data.Save_Scanner_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "Scanner");
        }

        public ActionResult Edit_Scanner(string id)
        {
            BL_Scanner Md_Com = new BL_Scanner();
            Mod_Scanner data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Scanner/Edit_Scanner.cshtml", data);
        }


        public ActionResult Update_Scanner(Mod_Scanner Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Scanner Md_Asset = new BL_Scanner();

                    status = Md_Asset.Save_Scanner_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("Scanner_Details", "Scanner");
        }


        public ActionResult Delete_Scanner(Mod_Scanner Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Scanner Md_Asset = new BL_Scanner();

                    status = Md_Asset.Save_Scanner_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Scanner_Details", "Scanner");
        }



    }
}