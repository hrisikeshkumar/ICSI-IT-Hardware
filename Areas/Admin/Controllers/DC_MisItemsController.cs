using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;


namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class DC_MisItemsController : Controller
    {

        public ActionResult DC_MisItems_Details()
        {
            BL_DC_MisItems com = new BL_DC_MisItems();

            List<Mod_DC_MisItems> pc_List = com.Get_DC_MisItemsData();

            return View("~/Areas/Admin/Views/DC_MisItems/DC_MisItems_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult DC_MisItems_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/DC_MisItems/DC_MisItems_Create_Item.cshtml");

        }

        [HttpPost]
        public ActionResult DC_MisItems_Create_Post(Mod_DC_MisItems Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_DC_MisItems save_data = new BL_DC_MisItems();
                    int status = save_data.Save_DC_MisItems_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "DC_MisItems");
        }

        public ActionResult Edit_DC_MisItems(string id)
        {
            BL_DC_MisItems Md_Com = new BL_DC_MisItems();
            Mod_DC_MisItems data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/DC_MisItems/Edit_DC_MisItems.cshtml", data);
        }


        public ActionResult Update_DC_MisItems(Mod_DC_MisItems Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_DC_MisItems Md_Asset = new BL_DC_MisItems();

                    status = Md_Asset.Save_DC_MisItems_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("DC_MisItems_Details", "DC_MisItems");
        }


        public ActionResult Delete_DC_MisItems(Mod_DC_MisItems Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_DC_MisItems Md_Asset = new BL_DC_MisItems();

                    status = Md_Asset.Save_DC_MisItems_data(Get_Data, "Delete", id);

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

            return RedirectToAction("DC_MisItems_Details", "DC_MisItems");
        }




    }
}