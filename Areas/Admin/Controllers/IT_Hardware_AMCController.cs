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

        public ActionResult Find_Warranty_Expired(string Types)
        {
           
            BL_Hardware_Amc B_Layer = new BL_Hardware_Amc();
           
            List<Mod_Warranty_Amc> mod_data = new List<Mod_Warranty_Amc>();

            if (Types == "Desktop")
            {
                mod_data = B_Layer.Find_Warranty_Expired("Desktop");
            }
            else if (Types == "Laptop")
            {
                mod_data = B_Layer.Find_Warranty_Expired("Laptop");
            }
            else if (Types == "Printer")
            {
                mod_data = B_Layer.Find_Warranty_Expired("Printer");
            }
            else if (Types == "Scanner")
            {
                mod_data = B_Layer.Find_Warranty_Expired("Scanner");
            }
            else if (Types == "Ups")
            {
                mod_data = B_Layer.Find_Warranty_Expired("Ups");
            }



            return View("~/Areas/Admin/Views/IT_Hardware_AMC/Amc_DashBoard.cshtml", mod_data);
        }

        public ActionResult Shift_Warnty_To_Amc(string Types)
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