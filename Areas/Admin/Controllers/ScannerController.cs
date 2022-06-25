using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class ScannerController : Controller
    {

        public ActionResult Scanner_Details()
        {
            BL_Scanner com = new BL_Scanner();

            List<Mod_Scanner> pc_List = com.Get_ScannerData();

            return View("~/Areas/Admin/Views/Scanner/Scanner_Details.cshtml", pc_List);
        }

        [HttpGet]
        public ActionResult Scanner_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            Mod_Scanner Mod_data = new Mod_Scanner();
            Item_MakeModel Make_List = new Item_MakeModel();
            Mod_data.Item_Make_List = Make_List.Item_MakeModel_List("PrintScan", "MAKE", "");


            return View("~/Areas/Admin/Views/Scanner/Scanner_Create_Item.cshtml", Mod_data);

        }

        [HttpPost]
        public ActionResult Scanner_CreateItem_Post(Mod_Scanner Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Scanner save_data = new BL_Scanner();
                    int status = save_data.Save_Scanner_data(Get_Data, "Add_new", "");

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

            return RedirectToAction("Create_Item", "Scanner");
        }

        public ActionResult Edit_Scanner(string id)
        {
            BL_Scanner BL_data = new BL_Scanner();
            Mod_Scanner Model_data = new Mod_Scanner();
            Item_MakeModel Make_List = new Item_MakeModel();

            Model_data = BL_data.Get_Data_By_ID(Model_data, id);

            Model_data.Item_Make_List = Make_List.Item_MakeModel_List("PrintScan", "MAKE", "");

            Model_data.Item_Model_List = Make_List.Item_MakeModel_List("PrintScan", "MODEL", Model_data.Item_Make_id.Trim().ToString());


            return View("~/Areas/Admin/Views/Scanner/Edit_Scanner.cshtml", Model_data);
        }


        public ActionResult Update_Scanner(Mod_Scanner Get_Data, string Item_id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Scanner Md_Asset = new BL_Scanner();

                    status = Md_Asset.Save_Scanner_data(Get_Data, "Update", Item_id);

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

            return RedirectToAction("Scanner_Details", "Scanner");
        }


        public ActionResult Delete_Scanner(Mod_Scanner Get_Data, string id)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {


                    BL_Scanner Md_Asset = new BL_Scanner();

                    status = Md_Asset.Save_Scanner_data(Get_Data, "Delete", id);

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

            return RedirectToAction("Scanner_Details", "Scanner");
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