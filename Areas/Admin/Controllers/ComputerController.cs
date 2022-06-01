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

            return View("~/Areas/Admin/Views/Computer/Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Computer/Create_Item.cshtml");
            
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

        public ActionResult Edit_Item()
        {
            return View("~/Areas/Admin/Views/Computer/Edit_Item.cshtml");
        }

        public ActionResult Edit_Item_Post()
        {
            return View("~/Areas/Admin/Views/Computer/Edit_Item.cshtml");
        }

        public ActionResult Get_Detail()
        {
            return View("~/Areas/Admin/Views/Computer/Get_Detail.cshtml");
        }

        public ActionResult Delete_Item()
        {
            return View();
        }

    }
}