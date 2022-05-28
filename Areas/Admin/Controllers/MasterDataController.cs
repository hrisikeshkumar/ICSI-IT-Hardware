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

                    status = Md_Asset.Save_data(Get_Data, "Add_new", "");

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



        public ActionResult Edit_Assets(string id)
        {
            
            BL_AssetMaster Md_Asset = new BL_AssetMaster();
            //Md_Asset.Get_Data_By_ID(Asset.Asset_ID.ToString().Trim());
            Mod_AssetMaster data = Md_Asset.Get_Data_By_ID(id);

            return View("~/Areas/Admin/Views/MasterData/Edit_Assets.cshtml", data);
        }

        [HttpPost]
        public ActionResult Update_master(Mod_AssetMaster Get_Data, string Asset_ID)
        {
            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_AssetMaster Md_Asset = new BL_AssetMaster();

                    status = Md_Asset.Save_data(Get_Data, "Update", Asset_ID);

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



    }
}