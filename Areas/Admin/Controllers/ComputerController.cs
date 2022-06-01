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
        public ActionResult Details()
        {
            BL_Computer com= new BL_Computer();

            List<Mod_Computer> pc_List = com.Get_CompData();

            return View("~/Areas/Admin/Views/Computer/Com_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Computer/Com_Create_Item.cshtml");
            
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

                    if (status == 1)
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
            
            return RedirectToAction("Create_Item", "Computer");
        }

        public ActionResult Edit_Item(string id)
        {
            BL_Computer Md_Com = new BL_Computer();
            Mod_Computer data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Computer/Com_Edit_Item.cshtml");
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

            return RedirectToAction("Get_Master_Data", "MasterData");
        }



        public ActionResult Get_Detail()
        {
            return View("~/Areas/Admin/Views/Computer/Com_Get_Detail.cshtml");
        }

        public ActionResult Delete_Item()
        {
            return View();
        }

    }
}