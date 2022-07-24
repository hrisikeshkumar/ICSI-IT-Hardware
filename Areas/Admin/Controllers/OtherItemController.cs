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
    public class OtherItemController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult OtherItem_Details()
        {
            BL_OtherItem com = new BL_OtherItem();

            List<Mod_OtherItem> pc_List = com.Get_OtherItemData();

            return View("~/Areas/Admin/Views/OtherItem/OtherItem_Details.cshtml", pc_List);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult OtherItem_Create_Item(string Message)
        {
            ViewBag.Message = Message;


            Mod_OtherItem Mod_data = new Mod_OtherItem();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("OtherItem", "MAKE", "");

            return View("~/Areas/Admin/Views/OtherItem/OtherItem_Create_Item.cshtml", Mod_data);

        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpPost]
        public ActionResult OtherItem_Create_Post(Mod_OtherItem Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_OtherItem save_data = new BL_OtherItem();
                    int status = save_data.Save_OtherItem_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("OtherItem_Create_Item", "OtherItem");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Edit_OtherItem(string id)
        {
            
            BL_OtherItem BL_data = new BL_OtherItem();
            Mod_OtherItem Model_data = new Mod_OtherItem();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("OtherItem", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("OtherItem", "MODEL", Model_data.Item_Make_id.Trim().ToString());


            return View("~/Areas/Admin/Views/OtherItem/Edit_OtherItem.cshtml", Model_data);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_OtherItem(Mod_OtherItem Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_OtherItem Md_Asset = new BL_OtherItem();

                    status = Md_Asset.Save_OtherItem_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("OtherItem_Details", "OtherItem");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Delete_OtherItem(Mod_OtherItem Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_OtherItem Md_Asset = new BL_OtherItem();

                    status = Md_Asset.Save_OtherItem_data(Get_Data, "Delete", id);

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

            return RedirectToAction("OtherItem_Details", "OtherItem");
        }

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("OtherItem", "MODEL", Item_Make);

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