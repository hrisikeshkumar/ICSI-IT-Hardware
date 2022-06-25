﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class ServerController : Controller
    {


        public ActionResult Server_Details()
        {
            BL_Server com = new BL_Server();

            List<Mod_Server> pc_List = com.Get_ServerData();

            return View("~/Areas/Admin/Views/Server/Server_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Server_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Server Mod_data = new Mod_Server();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("Server", "MAKE", "");

            return View("~/Areas/Admin/Views/Server/Server_Create_Item.cshtml", Mod_data);

        }

        [HttpPost]
        public ActionResult Server_CreateItem_Post(Mod_Server Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Server save_data = new BL_Server();
                    int status = save_data.Save_Server_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "Server");
        }

        public ActionResult Edit_Server(string id)
        {
           
            BL_Server BL_data = new BL_Server();
            Mod_Server Model_data = new Mod_Server();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("Server", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("Server", "MODEL", Model_data.Item_Make_id.Trim().ToString());

            return View("~/Areas/Admin/Views/Server/Edit_Server.cshtml", Model_data);
        }


        public ActionResult Update_Server(Mod_Server Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Server Md_Asset = new BL_Server();

                    status = Md_Asset.Save_Server_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("Server_Details", "Server");
        }


        public ActionResult Delete_Server(Mod_Server Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Server Md_Asset = new BL_Server();

                    status = Md_Asset.Save_Server_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Server_Details", "Server");
        }


        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("Server", "MODEL", Item_Make);

            return Json(Mod_Make.Item_Model_List, JsonRequestBehavior.AllowGet);

        }


    }
}