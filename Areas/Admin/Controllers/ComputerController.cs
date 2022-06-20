using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.Models;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{

    public class ComputerController : Controller
    {
        public ActionResult Com_Details()
        {
            BL_Computer com= new BL_Computer();

            List<Mod_Computer> pc_List = com.Get_CompData();

            return View("~/Areas/Admin/Views/Computer/Com_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Com_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Computer Mod_data = new Mod_Computer();
            BL_Computer data = new BL_Computer();
            Mod_data.Item_Make_List = data.Item_MakeModel_List("Desktop", "MAKE","");

            return View("~/Areas/Admin/Views/Computer/Com_Create_Item.cshtml", Mod_data);
            
        }

        [HttpPost]
        public ActionResult Create_Item_Post(Mod_Computer Get_Data)
        {
            string Message = "";
            try
            {

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

        public ActionResult Com_Edit_Item(string id)
        {
            BL_Computer Md_Com = new BL_Computer();
            Mod_Computer data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Computer/Com_Edit_Item.cshtml", data);
        }

      
        public ActionResult Update_Computer(Mod_Computer Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Computer Md_Asset = new BL_Computer();

                    status = Md_Asset.Save_Computer_data(Get_Data, "Update", Asset_ID);

                    if (status < 1)
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


        public ActionResult Delete_Item(Mod_Computer Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Computer Md_Asset = new BL_Computer();

                    status = Md_Asset.Save_Computer_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Com_Details", "Computer");
        }


        public JsonResult Model_List(string Item_Make)
        {

            BL_Computer data = new BL_Computer();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = data.Item_MakeModel_List("Desktop", "MODEL", Item_Make);

            return Json(Mod_Make.Item_Model_List, JsonRequestBehavior.AllowGet);

        }


    }



}