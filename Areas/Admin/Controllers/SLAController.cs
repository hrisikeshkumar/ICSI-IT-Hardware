using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;


namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class SLAController : Controller
    {


        public ActionResult SLA_Details()
        {
            BL_SLA com = new BL_SLA();

            List<Mod_SLA> SLA_List = com.Get_SLAData();

            return View("~/Areas/Admin/Views/SLA/SLA_Details.cshtml", SLA_List);
        }

        [HttpGet]
        public ActionResult SLA_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            BL_SLA BL_data = new BL_SLA();

            Mod_SLA Mod_data = new Mod_SLA();
            
            Mod_data.Vendor_List = BL_data.Vendor_List();


            return View("~/Areas/Admin/Views/SLA/SLA_Create_Item.cshtml", Mod_data);

        }

        [HttpPost]
        public ActionResult SLA_CreateItem_Post(Mod_SLA Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_SLA save_data = new BL_SLA();
                    int status = save_data.Save_SLA_data(Get_Data, "Add_new", "");

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
                    TempData["Message"] = String.Format("Required data are not provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("SLA_Create_Item", "SLA");
        }

        public ActionResult Edit_SLA(string id)
        {
            BL_SLA BL_data = new BL_SLA();

            Mod_SLA Mod_data = new Mod_SLA();
            BL_data.Get_Data_By_ID(Mod_data, id);

            Mod_data.Vendor_List = BL_data.Vendor_List();

            return View("~/Areas/Admin/Views/SLA/Edit_SLA.cshtml", Mod_data);
        }


        public ActionResult Update_SLA(Mod_SLA Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_SLA Md_Asset = new BL_SLA();

                    status = Md_Asset.Save_SLA_data(Get_Data, "Update", Item_id);

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
                    TempData["Message"] = String.Format("Required data are not provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("SLA_Details", "SLA");
        }


        public ActionResult Delete_SLA(Mod_SLA Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_SLA Md_Asset = new BL_SLA();

                    status = Md_Asset.Save_SLA_data(Get_Data, "Delete", id);

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

            return RedirectToAction("SLA_Details", "SLA");
        }


    }
}