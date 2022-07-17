using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class User_InfoController : Controller
    {


        public ActionResult User_Info_Details()
        {
            BL_User_Info com = new BL_User_Info();

            List<Mod_User_Info> pc_List = com.Get_User_InfoData();

            return View("~/Areas/Admin/Views/User_Info/User_Info_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult User_Info_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_User_Info Mod_data = new Mod_User_Info();
            

            return View("~/Areas/Admin/Views/User_Info/User_Info_Create_Item.cshtml", Mod_data);

        }

        [HttpPost]
        public ActionResult User_Info_CreateItem_Post(Mod_User_Info Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_User_Info save_data = new BL_User_Info();
                    int status = save_data.Save_User_Info_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("User_Info_Create_Item", "User_Info");
        }

        public ActionResult Edit_User_Info(string id)
        {

            BL_User_Info BL_data = new BL_User_Info();
            Mod_User_Info Model_data = new Mod_User_Info();
           
            Model_data = BL_data.Get_Data_By_ID(Model_data, id);


            return View("~/Areas/Admin/Views/User_Info/Edit_User_Info.cshtml", Model_data);
        }


        public ActionResult Update_User_Info(Mod_User_Info Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_User_Info Md_Asset = new BL_User_Info();

                    status = Md_Asset.Save_User_Info_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("User_Info_Details", "User_Info");
        }


        public ActionResult Delete_User_Info(Mod_User_Info Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_User_Info Md_Asset = new BL_User_Info();

                    status = Md_Asset.Save_User_Info_data(Get_Data, "Delete", id);

                    if (status < 1)
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

            return RedirectToAction("User_Info_Details", "User_Info");
        }


        public JsonResult Model_List(string Item_Make)
        {

            Item_MakeModel Make_List = new Item_MakeModel();

            Mod_Computer Mod_Make = new Mod_Computer();

            Mod_Make.Item_Model_List = Make_List.Item_MakeModel_List("PrintScan", "MODEL", Item_Make);

            return Json(Mod_Make.Item_Model_List, JsonRequestBehavior.AllowGet);

        }




    }

}