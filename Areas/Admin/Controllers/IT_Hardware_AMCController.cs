using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;


namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class IT_Hardware_AMCController : Controller
    {
        // GET: Admin/IT_Hardware_AMC
        public ActionResult Amc_DashBoard()
        {

            Mod_Amc_Dashboard mod_data = new Mod_Amc_Dashboard();
            BL_Hardware_Amc Get_data = new BL_Hardware_Amc();
            Get_data.Get_Amc_Data(mod_data);

           return View("~/Areas/Admin/Views/IT_Hardware_AMC/Amc_DashBoard.cshtml", mod_data);
        }

        public ActionResult Find_Warranty_Expired()
        {
            return View();
        }

        public ActionResult Shift_Warnty_To_Amc()
        {
            return View();
        }

        public ActionResult Find_Amc_To_Renew()
        {
            return View();
        }
        public ActionResult Update_All_Amc()
        {
            return View();
        }

        public ActionResult Find_Amc_To_Remove()
        {
            return View();
        }

        public ActionResult Remove_From_Amc()
        {
            return View();
        }


    }
}