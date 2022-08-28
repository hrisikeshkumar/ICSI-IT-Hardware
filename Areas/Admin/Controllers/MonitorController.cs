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
    public class MonitorController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Monitor_Details()
            {
                BL_Monitor com = new BL_Monitor();

                List<Mod_Monitor> pc_List = com.Get_MonitorData();

                return View("~/Areas/Admin/Views/Monitor/Monitor_Details.cshtml", pc_List);
            }

        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult Moitor_Create_Item(string Message)
            {
                ViewBag.Message = Message;

                Mod_Monitor Mod_data = new Mod_Monitor();
                Item_MakeModel Make_List = new Item_MakeModel();
                Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("Desktop", "MAKE", "");

                return View("~/Areas/Admin/Views/Monitor/Moitor_Create_Item.cshtml", Mod_data);

            }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpPost]
        public ActionResult Moitor_Create_Post(Mod_Monitor Get_Data)
            {
                string Message = "";
                try
                {
                    Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                    if (ModelState.IsValid)
                        {
                        BL_Monitor save_data = new BL_Monitor();
                        int status = save_data.Save_Monitor_data(Get_Data, "Add_new", "");

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

                return RedirectToAction("Moitor_Create_Item", "Monitor");
            }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Edit_Monitor(string id)
            {
                Item_MakeModel Make_List = new Item_MakeModel();
                BL_Monitor BL_data = new BL_Monitor();
                Mod_Monitor Model_data = new Mod_Monitor();
                

                Model_data = BL_data.Get_Data_By_ID(Model_data, id);
                Model_data.Item_Make_List = Make_List.Item_MakeModel_List("Desktop", "MAKE", "");
                Model_data.Item_Model_List = Make_List.Item_MakeModel_List("Desktop", "MODEL", Model_data.Item_Make_id.Trim().ToString());

                return View("~/Areas/Admin/Views/Monitor/Edit_Monitor.cshtml", Model_data);
            }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_Monitor(Mod_Monitor Get_Data, string Item_id)
            {
                int status = 0;
                try
                {
                    Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                    if (ModelState.IsValid)
                    {
                        BL_Monitor Md_Asset = new BL_Monitor();

                        status = Md_Asset.Save_Monitor_data(Get_Data, "Update", Item_id);

                        if (status >0)
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

                return RedirectToAction("Monitor_Details", "Monitor");
            }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Delete_Monitor(Mod_Monitor Get_Data, string id)
            {
                int status = 0;
                try
                {

                    if (ModelState.IsValid)
                    {


                        BL_Monitor Md_Asset = new BL_Monitor();

                        status = Md_Asset.Save_Monitor_data(Get_Data, "Delete", id);

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

                return RedirectToAction("Monitor_Details", "Monitor");
            }



        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("Desktop", "MODEL", Item_Make);

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