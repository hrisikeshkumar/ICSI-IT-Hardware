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

    public class AppleIpadController : Controller
    {
        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult AppleIpad_Details()
        {
            BL_AppleIpad com = new BL_AppleIpad();

            List<Mod_AppleIpad> pc_List = com.Get_AppleIpadData();

            return View("~/Areas/Admin/Views/AppleIpad/AppleIpad_Details.cshtml", pc_List);
        }

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult AppleIpad_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_AppleIpad Mod_data = new Mod_AppleIpad();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("Ipad", "MAKE", "");

            return View("~/Areas/Admin/Views/AppleIpad/AppleIpad_Create_Item.cshtml", Mod_data);

        }

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpPost]
        public ActionResult AppleIpad_CreateItem_Post(Mod_AppleIpad Get_Data)
        {
            string Message = "";
            try
            {


                if (ModelState.IsValid)
                {
                    BL_AppleIpad save_data = new BL_AppleIpad();
                    Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                    int status = save_data.Save_AppleIpad_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("AppleIpad_Create_Item", "AppleIpad");
        }

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Edit_AppleIpad(string id)
        {
            

            BL_AppleIpad BL_data = new BL_AppleIpad();
            Mod_AppleIpad Model_data = new Mod_AppleIpad();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("Desktop", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("Desktop", "MODEL", Model_data.Item_Make_id.Trim().ToString());


            return View("~/Areas/Admin/Views/AppleIpad/Edit_AppleIpad.cshtml", Model_data);
        }

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_AppleIpad(Mod_AppleIpad Get_Data, string Item_id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;

                if (ModelState.IsValid)
                {
                    BL_AppleIpad Md_Asset = new BL_AppleIpad();

                    status = Md_Asset.Save_AppleIpad_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("AppleIpad_Details", "AppleIpad");
        }

        [Authorize(Roles = "SU, Admin")]
        public ActionResult Delete_AppleIpad(Mod_AppleIpad Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_AppleIpad Md_Asset = new BL_AppleIpad();

                    status = Md_Asset.Save_AppleIpad_data(Get_Data, "Delete", id);

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

            return RedirectToAction("AppleIpad_Details", "AppleIpad");
        }

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("Ipad", "MODEL", Item_Make);

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