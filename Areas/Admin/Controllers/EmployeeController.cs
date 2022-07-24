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
    public class EmployeeController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Employee_Details()
        {
            BL_Employee data = new BL_Employee(); 

            List<Mod_Employee> Emp_List = data.Get_EmployeeData();

            return View("~/Areas/Admin/Views/Employee/Employee_Details.cshtml", Emp_List);
        }

        [Authorize(Roles = "SU, Admin")]
        [HttpGet]
        public ActionResult Create_Employee(string Message)
        {
            ViewBag.Message = Message;

            BL_Employee data = new BL_Employee();

            Mod_Employee Mod_emp = new Mod_Employee();

            Mod_emp.Emp_Type_List = data.Bind_EmpType();

            Mod_emp.Dept_List = data.Bind_Dept();

            return View("~/Areas/Admin/Views/Employee/Create_Employee.cshtml", Mod_emp);

        }

        [Authorize(Roles = "SU, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Employee_Create_Post(Mod_Employee Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Employee save_data = new BL_Employee();
                    int status = save_data.Save_Employee_data(Get_Data, "Add_new");

                    if (status == 1)
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
                    TempData["Message"] = String.Format("Data not Entered Properly");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Create_Employee", "Employee");
        }

        [Authorize(Roles = "SU, Admin")]
        public ActionResult Edit_Employee(string id)
        {
            BL_Employee Emp_Data = new BL_Employee();
            Mod_Employee Mod_emp = new Mod_Employee();

            Mod_emp.Emp_Type_List = Emp_Data.Bind_EmpType();
            Mod_emp.Dept_List = Emp_Data.Bind_Dept();
           
            Mod_emp = Emp_Data.Get_Data_By_ID(id, Mod_emp);

            Mod_emp.Designation_List = Emp_Data.Bind_Designation(Mod_emp.Emp_Type);

            return View("~/Areas/Admin/Views/Employee/Edit_Employee.cshtml", Mod_emp);
        }

        [Authorize(Roles = "SU, Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Update_Employee(Mod_Employee Get_Data)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Employee Md_Emp = new BL_Employee();

                    status = Md_Emp.Save_Employee_data(Get_Data, "Update");

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

            return RedirectToAction("Update_Employee", "Employee");
        }

        [Authorize(Roles = "SU, Admin")]
        public ActionResult Delete_Employee(Mod_Employee Get_Data)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Employee Md_Emp = new BL_Employee();

                    status = Md_Emp.Save_Employee_data(Get_Data, "Delete");

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

            return RedirectToAction("Employee_Details", "Employee");
        }

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public JsonResult Get_Designation(string Emp_Type)
        {

            BL_Employee data = new BL_Employee();

            Mod_Employee Mod_emp = new Mod_Employee();

            Mod_emp.Designation_List = data.Bind_Designation(Emp_Type);

            return Json(Mod_emp.Designation_List, JsonRequestBehavior.AllowGet);
          
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