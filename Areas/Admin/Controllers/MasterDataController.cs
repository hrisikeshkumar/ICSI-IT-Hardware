using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.Models;
using IT_Hardware_Aug2021.BusinessLayer;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class MasterDataController : Controller
    {
        // GET: Admin/MasterData
        public ActionResult AssetMakeModel()
        {
            return View();
        }


        public ActionResult Save_master(Mod_AssetMaster Get_Data)
        {
            string Message = "";
            try
            {

                if (ModelState.IsValid)
                {
                    BL_AssetMasterData BL_Asset = new BL_AssetMasterData();
                    int status = 0;
                        //BL_Asset.Save_Item(Get_Data, "Add_new");

                    if (status == 1)
                    {
                        TempData["Message"] = String.Format("Data is not saved");

                    }
                    else
                    {

                        TempData["Message"] = String.Format("Data have saved successfully");

                    }
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Create_Item", "Computer");
        }


    }
}