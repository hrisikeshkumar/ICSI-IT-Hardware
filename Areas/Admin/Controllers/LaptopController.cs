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
    public class LaptopController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Lap_Details()
        {
            BL_Laptop com = new BL_Laptop();

            List<Mod_Laptop> pc_List = com.Get_CompData();

            return View("~/Areas/Admin/Views/Laptop/Lap_Details.cshtml", pc_List);
        }


        [HttpGet]
        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Lap_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Laptop Mod_data = new Mod_Laptop();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("Laptop", "MAKE", "");

            return View("~/Areas/Admin/Views/Laptop/Lap_Create_Item.cshtml", Mod_data);
        }

        [HttpPost]
        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Lap_Create_Post(Mod_Laptop Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Laptop save_data = new BL_Laptop();
                    int status = save_data.Save_Laptop_data(Get_Data, "Add_new", "");

                    if (status < 1)
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                    else
                    {

                        TempData["Message"] = String.Format("Data save successfully");
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

            return RedirectToAction("Lap_Create_Item", "Laptop");
        }
        

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Edit_Laptop(string id)
        {
            BL_Laptop BL_data = new BL_Laptop();
            Mod_Laptop Model_data = new Mod_Laptop();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("Laptop", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("Laptop", "MODEL", Model_data.Item_Make_id.Trim().ToString());


            return View("~/Areas/Admin/Views/Laptop/Edit_Laptop.cshtml", Model_data);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_Laptop(Mod_Laptop Get_Data, string Item_id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Laptop Md_Asset = new BL_Laptop();

                    status = Md_Asset.Save_Laptop_data(Get_Data, "Update", Item_id);

                    if (status == 1)
                    {
                        TempData["Message"] = String.Format("Data have saved successfully");
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

            return RedirectToAction("Lap_Details", "Laptop");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Delete_Laptop(Mod_Laptop Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Laptop Md_Asset = new BL_Laptop();

                    status = Md_Asset.Save_Laptop_data(Get_Data, "Delete", id);

                    if (status > 1)
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


        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("Laptop", "MODEL", Item_Make);

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