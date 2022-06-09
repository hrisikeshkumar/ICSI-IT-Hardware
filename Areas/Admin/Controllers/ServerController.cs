using System;
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

            return View("~/Areas/Admin/Views/Server/Server_Create_Item.cshtml");

        }

        [HttpPost]
        public ActionResult Lap_Create_Post(Mod_Server Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Server save_data = new BL_Server();
                    int status = save_data.Save_Server_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "Server");
        }

        public ActionResult Edit_Server(string id)
        {
            BL_Server Md_Com = new BL_Server();
            Mod_Server data = Md_Com.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/Server/Edit_Server.cshtml", data);
        }


        public ActionResult Update_Server(Mod_Server Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Server Md_Asset = new BL_Server();

                    status = Md_Asset.Save_Server_data(Get_Data, "Update", Asset_ID);

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



    }
}