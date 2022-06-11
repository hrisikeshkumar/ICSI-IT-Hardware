using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {


        public ActionResult Employee_Details()
        {
            BL_Employee data = new BL_Employee(); 

            List<Mod_Employee> pc_List = data.Get_EmployeeData();

            return View("~/Areas/Admin/Views/Employee/Employee_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Employee_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            BL_Employee data = new BL_Employee();

            Mod_Employee Mod_emp = new Mod_Employee();

            Mod_emp.Emp_Type_List = data.Bind_EmpType();

            //Mod_emp.Designation_List = data.Bind_Designation();

            return View("~/Areas/Admin/Views/Employee/Employee_Create_Item.cshtml", Mod_emp);

        }

        [HttpPost]
        public ActionResult Employee_Create_Post(Mod_Employee Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Employee save_data = new BL_Employee();
                    int status = save_data.Save_Employee_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "Employee");
        }

        public ActionResult Edit_Employee(string id)
        {
            BL_Employee Md_Com = new BL_Employee();
            Mod_Employee data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Employee/Edit_Employee.cshtml", data);
        }


        public ActionResult Update_Employee(Mod_Employee Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Employee Md_Asset = new BL_Employee();

                    status = Md_Asset.Save_Employee_data(Get_Data, "Update", Asset_ID);

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

            return RedirectToAction("Employee_Details", "Employee");
        }


        public ActionResult Delete_Employee(Mod_Employee Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Employee Md_Asset = new BL_Employee();

                    status = Md_Asset.Save_Employee_data(Get_Data, "Delete", id);

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


        public JsonResult Get_Designation(string Emp_Type)
        {

            BL_Employee data = new BL_Employee();

            Mod_Employee Mod_emp = new Mod_Employee();

            Mod_emp.Emp_Type_List = data.Bind_EmpType();

            Mod_emp.Designation_List = data.Bind_Designation(Emp_Type);

            return Json(Mod_emp.Designation_List, JsonRequestBehavior.AllowGet);
          
        }

    }
}