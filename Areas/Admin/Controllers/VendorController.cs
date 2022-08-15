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
    public class VendorController : Controller
    {
        [Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public ActionResult Vendor_Details()
        {
            BL_Vendor com = new BL_Vendor();

            List<Mod_Vendor> pc_List = com.Get_VendorData();

            return View("~/Areas/Admin/Views/Vendor/Vendor_Details.cshtml", pc_List);
        }


        [Authorize(Roles = "SU, Admin, Manager")]
        [HttpGet]
        public ActionResult Vendor_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Vendor/Vendor_Create_Item.cshtml");

        }


        [Authorize(Roles = "SU, Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vendor_Create_Post(Mod_Vendor Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Vendor save_data = new BL_Vendor();
                    int status = save_data.Save_Vendor_data(Get_Data, "Add_new", "");

                    if (status > 0)
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

            return RedirectToAction("Vendor_Create_Item", "Vendor");
        }


        [Authorize(Roles = "SU, Admin, Manager")]
        public ActionResult Edit_Vendor(string id)
        {
            BL_Vendor Md_Com = new BL_Vendor();
            Mod_Vendor data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Vendor/Edit_Vendor.cshtml", data);
        }



        [Authorize(Roles = "SU, Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update_Vendor(Mod_Vendor Get_Data)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Vendor Md_Asset = new BL_Vendor();

                    status = Md_Asset.Save_Vendor_data(Get_Data, "Update", Get_Data.Vendor_id);

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

            return RedirectToAction("Vendor_Details", "Vendor");
        }



        [Authorize(Roles = "SU, Admin, Manager")]
        public ActionResult Delete_Vendor(Mod_Vendor Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Vendor Md_Asset = new BL_Vendor();

                    status = Md_Asset.Save_Vendor_data(Get_Data, "Delete", id);

                    if (status >0)
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

            return RedirectToAction("Vendor_Details", "Vendor");
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