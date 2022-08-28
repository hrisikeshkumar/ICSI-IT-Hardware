using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.Models;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using System.Web.Mvc.Filters;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{

    public class ComputerController : Controller
    {
        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Com_Details()
        {
            BL_Computer com= new BL_Computer();

            List<Mod_Computer> pc_List = com.Get_CompData();

            return View("~/Areas/Admin/Views/Computer/Com_Details.cshtml", pc_List);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult Com_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Computer Mod_data = new Mod_Computer();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("Desktop", "MAKE","");


            return View("~/Areas/Admin/Views/Computer/Com_Create_Item.cshtml", Mod_data);
            
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpPost]
        public ActionResult Create_Item_Post(Mod_Computer Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;

                if (ModelState.IsValid)
                {
                    BL_Computer save_data = new BL_Computer();
                    int status = save_data.Save_Computer_data(Get_Data, "Add_new", "");

                    if (status < 1)
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                    else {

                        TempData["Message"] = String.Format("Data save successfully");
                    }
                }
            }
            catch (Exception ex) {

                TempData["Message"] = string.Format("ShowFailure();");
                
            }
            
            return RedirectToAction("Com_Create_Item", "Computer");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Com_Edit_Item(string id)
        {
            BL_Computer BL_data = new BL_Computer();
            Mod_Computer Model_data = new Mod_Computer();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);
            
            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("Desktop", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("Desktop", "MODEL", Model_data.Item_Make_id.Trim().ToString());

            return View("~/Areas/Admin/Views/Computer/Com_Edit_Item.cshtml", Model_data);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_Computer(Mod_Computer Get_Data, string Item_id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Computer Md_Asset = new BL_Computer();

                    status = Md_Asset.Save_Computer_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("Com_Details", "Computer");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Delete_Item(Mod_Computer Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Computer Md_Asset = new BL_Computer();

                    status = Md_Asset.Save_Computer_data(Get_Data, "Delete", Item_id);

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

            return RedirectToAction("Com_Details", "Computer");
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