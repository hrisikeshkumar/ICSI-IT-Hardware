using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;


namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class SwitchController : Controller
    {


        public ActionResult Switch_Details()
        {
            BL_Switch com = new BL_Switch();

            List<Mod_Switch> pc_List = com.Get_SwitchData();

            return View("~/Areas/Admin/Views/Switch/Switch_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Switch_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Switch/Switch_Create_Item.cshtml");

        }

        [HttpPost]
        public ActionResult Lap_Create_Post(Mod_Switch Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Switch save_data = new BL_Switch();
                    int status = save_data.Save_Switch_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "Switch");
        }

        public ActionResult Edit_Switch(string id)
        {
            BL_Switch Md_Com = new BL_Switch();
            Mod_Switch data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Switch/Edit_Switch.cshtml", data);
        }


        public ActionResult Update_Switch(Mod_Switch Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Switch Md_Asset = new BL_Switch();

                    status = Md_Asset.Save_Switch_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("Switch_Details", "Switch");
        }


        public ActionResult Delete_Switch(Mod_Switch Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Switch Md_Asset = new BL_Switch();

                    status = Md_Asset.Save_Switch_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Switch_Details", "Switch");
        }


    }
}