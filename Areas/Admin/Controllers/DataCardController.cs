using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class DataCardController : Controller
    {


        public ActionResult DataCard_Details()
        {
            BL_DataCard com = new BL_DataCard();

            List<Mod_DataCard> pc_List = com.Get_DataCardData();

            return View("~/Areas/Admin/Views/DataCard/DataCard_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Lap_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/DataCard/DataCard_Create_Item.cshtml");

        }

        [HttpPost]
        public ActionResult Lap_Create_Post(Mod_DataCard Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_DataCard save_data = new BL_DataCard();
                    int status = save_data.Save_DataCard_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "DataCard");
        }

        public ActionResult Edit_DataCard(string id)
        {
            BL_DataCard Md_Com = new BL_DataCard();
            Mod_DataCard data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/DataCard/Edit_DataCard.cshtml", data);
        }


        public ActionResult Update_DataCard(Mod_DataCard Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_DataCard Md_Asset = new BL_DataCard();

                    status = Md_Asset.Save_DataCard_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("DataCard_Details", "DataCard");
        }


        public ActionResult Delete_DataCard(Mod_DataCard Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_DataCard Md_Asset = new BL_DataCard();

                    status = Md_Asset.Save_DataCard_data(Get_Data, "Delete", id);

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

            return RedirectToAction("DataCard_Details", "DataCard");
        }


    }
}