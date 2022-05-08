using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.Models;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;


namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class MasterDataController : Controller
    {
        // GET: Admin/MasterData
        //public ActionResult AssetMakeModel()
        //{
        //    return View();
        //}

        public ActionResult Get_Master_Data()
        {
            BL_AssetMaster master_data = new BL_AssetMaster();

            List<Mod_AssetMaster> MasterList = master_data.Get_Data();

            return View("~/Areas/Admin/Views/MasterData/Get_Master_Data.cshtml", MasterList);
        }


        [HttpGet]
        public ActionResult Add_Data(Mod_AssetMaster Get_Data)
        {
            return View("~/Areas/Admin/Views/MasterData/AssetMakeModel.cshtml");
        }


        [HttpPost]
        public ActionResult Save_master(Mod_AssetMaster Get_Data)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_AssetMaster Md_Asset = new BL_AssetMaster();

                    status = Md_Asset.Save_data(Get_Data);

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

            return RedirectToAction("Create_Item", "Computer");
        }



    }
}