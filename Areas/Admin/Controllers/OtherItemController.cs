using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;


namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class OtherItemController : Controller
    {

        public ActionResult OtherItem_Details()
        {
            BL_OtherItem com = new BL_OtherItem();

            List<Mod_OtherItem> pc_List = com.Get_OtherItemData();

            return View("~/Areas/Admin/Views/OtherItem/OtherItem_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult OtherItem_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/OtherItem/OtherItem_Create_Item.cshtml");

        }

        [HttpPost]
        public ActionResult OtherItem_Create_Post(Mod_OtherItem Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_OtherItem save_data = new BL_OtherItem();
                    int status = save_data.Save_OtherItem_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "OtherItem");
        }

        public ActionResult Edit_OtherItem(string id)
        {
            BL_OtherItem Md_Com = new BL_OtherItem();
            Mod_OtherItem data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/OtherItem/Edit_OtherItem.cshtml", data);
        }


        public ActionResult Update_OtherItem(Mod_OtherItem Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_OtherItem Md_Asset = new BL_OtherItem();

                    status = Md_Asset.Save_OtherItem_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("OtherItem_Details", "OtherItem");
        }


        public ActionResult Delete_OtherItem(Mod_OtherItem Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_OtherItem Md_Asset = new BL_OtherItem();

                    status = Md_Asset.Save_OtherItem_data(Get_Data, "Delete", id);

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

            return RedirectToAction("OtherItem_Details", "OtherItem");
        }
    }
}