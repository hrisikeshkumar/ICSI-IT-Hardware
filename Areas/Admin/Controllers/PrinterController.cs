using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class PrinterController : Controller
    {
       

            public ActionResult Printer_Details()
            {
                BL_Printer com = new BL_Printer();

                List<Mod_Printer> pc_List = com.Get_PrinterData();

                return View("~/Areas/Admin/Views/Printer/Printer_Details.cshtml", pc_List);
            }

            [HttpGet]
            public ActionResult Lap_Create_Item(string Message)
            {
                ViewBag.Message = Message;

                return View("~/Areas/Admin/Views/Printer/Printer_Create_Item.cshtml");

            }

            [HttpPost]
            public ActionResult Lap_Create_Post(Mod_Printer Get_Data)
            {
                string Message = "";
                try
                {

                    if (ModelState.IsValid)
                    {
                        BL_Printer save_data = new BL_Printer();
                        int status = save_data.Save_Printer_data(Get_Data, "Add_new", "");

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

                return RedirectToAction("Create_Item", "Printer");
            }

            public ActionResult Edit_Printer(string id)
            {
                BL_Printer Md_Com = new BL_Printer();
                Mod_Printer data = Md_Com.Get_Data_By_ID(id);

                return View("~/Areas/Admin/Views/Printer/Edit_Printer.cshtml", data);
            }


            public ActionResult Update_Printer(Mod_Printer Get_Data, string Asset_ID)
            {
                int status = 0;
                try
                {

                    if (ModelState.IsValid)
                    {
                        BL_Printer Md_Asset = new BL_Printer();

                        status = Md_Asset.Save_Printer_data(Get_Data, "Update", Asset_ID);

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

                return RedirectToAction("Printer_Details", "Printer");
            }


            public ActionResult Delete_Printer(Mod_Printer Get_Data, string id)
            {
                int status = 0;
                try
                {

                    if (ModelState.IsValid)
                    {


                        BL_Printer Md_Asset = new BL_Printer();

                        status = Md_Asset.Save_Printer_data(Get_Data, "Delete", id);

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

                return RedirectToAction("Printer_Details", "Printer");
            }




        }
   
}