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
    public class Item_IssueController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Item_Issue_Details()
        {
            BL_Item_Issue com = new BL_Item_Issue();

            List<Mod_Item_Issue> pc_List = com.Get_Item_IssueData();

            return View("~/Areas/Admin/Views/Item_Issue/Item_Issue_Details.cshtml", pc_List);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpGet]
        public ActionResult Item_Issue_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Item_Issue/Item_Issue_Create_Item.cshtml");

        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        [HttpPost]
        public ActionResult Item_Issue_Create_Post(Mod_Item_Issue Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Item_Issue save_data = new BL_Item_Issue();
                    int status = save_data.Save_Item_Issue_data(Get_Data, "Add_new", "");

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

                    TempData["Message"] = String.Format("Required field are not provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Item_Issue_Create_Item", "Item_Issue");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Edit_Item_Issue(string id)
        {
            BL_Item_Issue Md_Com = new BL_Item_Issue();
            Mod_Item_Issue data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Item_Issue/Edit_Item_Issue.cshtml", data);
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Update_Item_Issue(Mod_Item_Issue Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Item_Issue Md_Asset = new BL_Item_Issue();

                    status = Md_Asset.Save_Item_Issue_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("Item_Issue_Details", "Item_Issue");
        }


        [Authorize(Roles = "SU, Admin, InventoryManager")]
        public ActionResult Delete_Item_Issue(Mod_Item_Issue Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Item_Issue Md_Asset = new BL_Item_Issue();

                    status = Md_Asset.Save_Item_Issue_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Item_Issue_Details", "Item_Issue");
        }


        [HttpPost]
        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public JsonResult AutoComplete(string SL_No)
        {

            BL_Item_Issue data = new BL_Item_Issue();
            List<Item_SL_Wise> list = data.Item_SLnumber_List(SL_No);


            return Json(list);
        }


        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        [HttpPost]
        public JsonResult Find_Item_Issue(string Item_Id)
        {

            Mod_Item_Issue_Employee Emp_Details= new Mod_Item_Issue_Employee();

            BL_Item_Issue Item_data = new BL_Item_Issue();

            Emp_Details = Item_data.Issue_Employee(Emp_Details, Item_Id, "Item_Issue");




            return Json(Emp_Details);

        }


        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        [HttpPost]
        public JsonResult AutoComplete_TransferEmployee(string EmpID)
        {

            BL_Item_Issue data = new BL_Item_Issue();
            List<Mod_Item_Issue_Employee> list = data.Emp_List(EmpID);

            return Json(list);
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