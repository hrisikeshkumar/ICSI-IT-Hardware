﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class PrinterController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Printer_Details()
            {
                BL_Printer com = new BL_Printer();

                List<Mod_Printer> pc_List = com.Get_PrinterData();

                return View("~/Areas/Admin/Views/Printer/Printer_Details.cshtml", pc_List);
            }



        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult Printer_Create_Item(string Message)
            {
                ViewBag.Message = Message;

                Mod_Printer Mod_data = new Mod_Printer();
                Item_MakeModel Make_List = new Item_MakeModel();
                Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("PrintScan", "MAKE", "");


            return View("~/Areas/Admin/Views/Printer/Printer_Create_Item.cshtml", Mod_data);

            }



        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpPost]
        public ActionResult Printer_CreateItem_Post(Mod_Printer Get_Data)
            {
                string Message = "";
                try
                {
                    Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                    if (ModelState.IsValid)
                    {
                        BL_Printer save_data = new BL_Printer();
                        int status = save_data.Save_Printer_data(Get_Data, "Add_new", "");

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
                        TempData["Message"] = String.Format("Required Data are not Provided");
                    }
                }
                catch (Exception ex)
                {

                    TempData["Message"] = string.Format("ShowFailure();");

                }

                return RedirectToAction("Printer_Create_Item", "Printer");
            }



        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Edit_Printer(string id)
            {
                
                BL_Printer BL_data = new BL_Printer();
                Mod_Printer Model_data = new Mod_Printer();
                Item_MakeModel Make_List = new Item_MakeModel();

                Model_data = BL_data.Get_Data_By_ID(Model_data, id);

                Model_data.Item_Make_List = Make_List.Item_MakeModel_List("PrintScan", "MAKE", "");

                Model_data.Item_Model_List = Make_List.Item_MakeModel_List("PrintScan", "MODEL", Model_data.Item_Make_id.Trim().ToString());


            return View("~/Areas/Admin/Views/Printer/Edit_Printer.cshtml", Model_data);
            }



        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_Printer(Mod_Printer Get_Data, string Item_id)
            {
                int status = 0;
                try
                {
                    Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                    if (ModelState.IsValid)
                    {
                        BL_Printer Md_Asset = new BL_Printer();

                        status = Md_Asset.Save_Printer_data(Get_Data, "Update", Item_id);

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
                        TempData["Message"] = String.Format("Required Data are not Provided");
                    }
                }
                catch (Exception ex)
                {

                    TempData["Message"] = string.Format("ShowFailure();");

                }

                return RedirectToAction("Printer_Details", "Printer");
            }



        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Delete_Printer(Mod_Printer Get_Data, string id)
            {
                int status = 0;
                try
                {
                    Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                    if (ModelState.IsValid)
                    {


                        BL_Printer Md_Asset = new BL_Printer();

                        status = Md_Asset.Save_Printer_data(Get_Data, "Delete", id);

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

                return RedirectToAction("Printer_Details", "Printer");
            }


        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("PrintScan", "MODEL", Item_Make);

            return Json(Mod_Make.Item_Model_List, JsonRequestBehavior.AllowGet);

        }



        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {

                if (filterContext.Result is HttpUnauthorizedResult)
                {
                    filterContext.Result = new RedirectResult("~/Authorization/AccessDedied");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Log_In/Log_In");
            }
        }


    }

}