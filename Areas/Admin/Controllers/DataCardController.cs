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
    public class DataCardController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult DataCard_Details()
        {
            BL_DataCard com = new BL_DataCard();

            List<Mod_DataCard> pc_List = com.Get_DataCardData();

            return View("~/Areas/Admin/Views/DataCard/DataCard_Details.cshtml", pc_List);
        }

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult DataCard_Create_Item(string Message)
        {
            ViewBag.Message = Message;
            Mod_DataCard Mod_data = new Mod_DataCard();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("DataCard", "MAKE", "");


            return View("~/Areas/Admin/Views/DataCard/DataCard_Create_Item.cshtml", Mod_data);

        }

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpPost]
        public ActionResult DataCard_CreateItem_Post(Mod_DataCard Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_DataCard save_data = new BL_DataCard();
                    int status = save_data.Save_DataCard_data(Get_Data, "Add_new", "");

                    if (status < 1)
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

            return RedirectToAction("DataCard_Create_Item", "DataCard");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Edit_DataCard(string id)
        {
            

            BL_DataCard BL_data = new BL_DataCard();
            Mod_DataCard Model_data = new Mod_DataCard();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("DataCard", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("DataCard", "MODEL", Model_data.Item_Make_id.Trim().ToString());


            return View("~/Areas/Admin/Views/DataCard/Edit_DataCard.cshtml", Model_data);
        }

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_DataCard(Mod_DataCard Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_DataCard Md_Asset = new BL_DataCard();

                    status = Md_Asset.Save_DataCard_data(Get_Data, "Update", Item_id);

                    if (status > 0)
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

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Delete_DataCard(Mod_DataCard Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_DataCard Md_Asset = new BL_DataCard();

                    status = Md_Asset.Save_DataCard_data(Get_Data, "Delete", id);

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

            return RedirectToAction("DataCard_Details", "DataCard");
        }

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("DataCard", "MODEL", Item_Make);

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