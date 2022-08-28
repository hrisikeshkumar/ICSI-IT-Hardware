using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Shift_ItemController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Shift_Item_Details()
        {
            BL_Shift_Item com = new BL_Shift_Item();

            List<Mod_Shift_Item> pc_List = com.Get_Shift_ItemData();

            return View("~/Areas/Admin/Views/Shift_Item/Shift_Item_Details.cshtml", pc_List);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult Shift_Item_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Shift_Item/Shift_Item_Create_Item.cshtml");

        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpPost]
        public ActionResult Shift_Item_Create_Post(Mod_Shift_Item Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Shift_Item save_data = new BL_Shift_Item();
                    int status = save_data.Save_Shift_Item_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "Shift_Item");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Edit_Shift_Item(string id)
        {
            BL_Shift_Item Md_Com = new BL_Shift_Item();
            Mod_Shift_Item data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Shift_Item/Edit_Shift_Item.cshtml", data);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_Shift_Item(Mod_Shift_Item Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Shift_Item Md_Asset = new BL_Shift_Item();

                    status = Md_Asset.Save_Shift_Item_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("Shift_Item_Details", "Shift_Item");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Delete_Shift_Item(Mod_Shift_Item Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Shift_Item Md_Asset = new BL_Shift_Item();

                    status = Md_Asset.Save_Shift_Item_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Shift_Item_Details", "Shift_Item");
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