﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class LaptopController : Controller
    {


        public ActionResult Lap_Details()
        {
            BL_Laptop com = new BL_Laptop();

            List<Mod_Laptop> pc_List = com.Get_CompData();

            return View("~/Areas/Admin/Views/Laptop/Lap_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Lap_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Laptop/Lap_Create_Item.cshtml");

        }

        [HttpPost]
        public ActionResult Lap_Create_Post(Mod_Laptop Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Laptop save_data = new BL_Laptop();
                    int status = save_data.Save_Laptop_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "Laptop");
        }

        public ActionResult Edit_Laptop(string id)
        {
            BL_Laptop Md_Com = new BL_Laptop();
            Mod_Laptop data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Laptop/Edit_Laptop.cshtml", data);
        }


        public ActionResult Update_Laptop(Mod_Laptop Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Laptop Md_Asset = new BL_Laptop();

                    status = Md_Asset.Save_Laptop_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("Lap_Details", "Laptop");
        }


        public ActionResult Delete_Laptop(Mod_Laptop Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Laptop Md_Asset = new BL_Laptop();

                    status = Md_Asset.Save_Laptop_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Lap_Details", "Laptop");
        }




    }
}