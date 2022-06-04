﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class MonitorController : Controller
    {


            public ActionResult Monitor_Details()
            {
                BL_Monitor com = new BL_Monitor();

                List<Mod_Monitor> pc_List = com.Get_MonitorData();

                return View("~/Areas/Admin/Views/Monitor/Monitor_Details.cshtml", pc_List);
            }

            [HttpGet]
            public ActionResult Moitor_Create_Item(string Message)
            {
                ViewBag.Message = Message;

                return View("~/Areas/Admin/Views/Monitor/Moitor_Create_Item.cshtml");

            }

            [HttpPost]
            public ActionResult Lap_Create_Post(Mod_Monitor Get_Data)
            {
                string Message = "";
                try
                {

                    if (ModelState.IsValid)
                    {
                        BL_Monitor save_data = new BL_Monitor();
                        int status = save_data.Save_Monitor_data(Get_Data, "Add_new", "");

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

                return RedirectToAction("Create_Item", "Monitor");
            }

            public ActionResult Edit_Monitor(string id)
            {
                BL_Monitor Md_Com = new BL_Monitor();
                Mod_Monitor data = Md_Com.Get_Data_By_ID(id);

                return View("~/Areas/Admin/Views/Monitor/Edit_Monitor.cshtml", data);
            }


            public ActionResult Update_Monitor(Mod_Monitor Get_Data, string Asset_ID)
            {
                int status = 0;
                try
                {

                    if (ModelState.IsValid)
                    {
                        BL_Monitor Md_Asset = new BL_Monitor();

                        status = Md_Asset.Save_Monitor_data(Get_Data, "Update", Asset_ID);

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

                return RedirectToAction("Monitor_Details", "Monitor");
            }


            public ActionResult Delete_Monitor(Mod_Monitor Get_Data, string id)
            {
                int status = 0;
                try
                {

                    if (ModelState.IsValid)
                    {


                        BL_Monitor Md_Asset = new BL_Monitor();

                        status = Md_Asset.Save_Monitor_data(Get_Data, "Delete", id);

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

                return RedirectToAction("Monitor_Details", "Monitor");
            }




        
    }
}