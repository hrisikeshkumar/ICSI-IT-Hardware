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
    public class SwitchController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Switch_Details()
        {
            BL_Switch com = new BL_Switch();

            List<Mod_Switch> pc_List = com.Get_SwitchData();

            return View("~/Areas/Admin/Views/Switch/Switch_Details.cshtml", pc_List);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult Switch_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Switch Mod_data = new Mod_Switch();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("Switch", "MAKE", "");

            return View("~/Areas/Admin/Views/Switch/Switch_Create_Item.cshtml", Mod_data);

        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpPost]
        public ActionResult Switch_CreateItem_Post(Mod_Switch Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Switch save_data = new BL_Switch();
                    int status = save_data.Save_Switch_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Switch_Create_Item", "Switch");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Edit_Switch(string id)
        {
           
            BL_Switch BL_data = new BL_Switch();
            Mod_Switch Model_data = new Mod_Switch();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("Switch", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("Switch", "MODEL", Model_data.Item_Make_id.Trim().ToString());


            return View("~/Areas/Admin/Views/Switch/Edit_Switch.cshtml", Model_data);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_Switch(Mod_Switch Get_Data, string Item_id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Switch Md_Asset = new BL_Switch();

                    status = Md_Asset.Save_Switch_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("Switch_Details", "Switch");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Delete_Switch(Mod_Switch Get_Data, string id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
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


        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("Switch", "MODEL", Item_Make);

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