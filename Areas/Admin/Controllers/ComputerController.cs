using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.BusinessLayer;
using IT_Hardware_Aug2021.Areas.Admin.Models;


namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{

    
    public class ComputerController : Controller
    {

        //static string Data_Transfer = string.Empty;

        // GET: Admin/Computer

        public ActionResult Details()
        {
            ComputerBusinessLayer com= new ComputerBusinessLayer();

            List<DesktopPC> pc_List = com.Get_Desktop_list();

            return View("~/Areas/Admin/Views/Computer/Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Computer/Create_Item.cshtml");
            //return View();
        }

        [HttpPost]
        public ActionResult Create_Item_Post(DesktopPC Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    ComputerBusinessLayer save_data = new ComputerBusinessLayer();
                    int status = save_data.Save_Item(Get_Data, "Add_new");

                    if (status == 1)
                    {
                        TempData["Message"] = String.Format("Data is not saved");

                    }
                    else {

                        TempData["Message"] = String.Format("Data have saved successfully");

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