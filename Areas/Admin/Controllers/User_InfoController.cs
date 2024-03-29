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
    public class User_InfoController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult User_Info_Details()
        {
            BL_User_Info com = new BL_User_Info();

            List<Mod_User_Info> data = com.Get_User_InfoData();

            return View("~/Areas/Admin/Views/User_Info/User_Info_Details.cshtml", data);
        }

        [Authorize(Roles = "SU, Admin")]
        [HttpGet]
        public ActionResult User_Info_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_User_Info Mod_data = new Mod_User_Info();
            

            return View("~/Areas/Admin/Views/User_Info/User_Info_Create_Item.cshtml", Mod_data);

        }


        [Authorize(Roles = "SU, Admin")]
        [HttpPost]
        public ActionResult User_Info_CreateItem_Post(Mod_User_Info Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_User_Info save_data = new BL_User_Info();
                    int status = save_data.Save_User_Info_data(Get_Data, "Add_new", "");

                    if (status > 0)
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

            return RedirectToAction("User_Info_Create_Item", "User_Info");
        }


        [Authorize(Roles = "SU, Admin")]
        public ActionResult Edit_User_Info(string id)
        {

            BL_User_Info BL_data = new BL_User_Info();
            Mod_User_Info Model_data = new Mod_User_Info();
           
            Model_data = BL_data.Get_Data_By_ID(Model_data, id);


            return View("~/Areas/Admin/Views/User_Info/Edit_User_Info.cshtml", Model_data);
        }


        [Authorize(Roles = "SU, Admin")]
        public ActionResult Update_User_Info(Mod_User_Info Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_User_Info Md_Asset = new BL_User_Info();

                    status = Md_Asset.Save_User_Info_data(Get_Data, "Update", Item_id);

                    if (status > 0)
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

            return RedirectToAction("User_Info_Details", "User_Info");
        }


        [Authorize(Roles = "SU")]
        public ActionResult Delete_User_Info(Mod_User_Info Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_User_Info Md_Asset = new BL_User_Info();

                    status = Md_Asset.Save_User_Info_data(Get_Data, "Delete", id);

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

            return RedirectToAction("User_Info_Details", "User_Info");
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