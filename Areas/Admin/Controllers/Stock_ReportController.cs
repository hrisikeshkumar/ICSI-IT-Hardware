using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Stock_ReportController : Controller
    {
        
        public ActionResult Stock_Report_Detail()
        {
            return View("~/Areas/Admin/Views/Stock_Report/Stock_Report_Detail.cshtml");
        }


        public ActionResult Post_Stock_Report(string Item_Type , string Report_Type)
        {
            return View();
        }

        public ActionResult Budget_Report_Detail()
        {
            return View("~/Areas/Admin/Views/Stock_Report/Budget_Report_Detail.cshtml");
        }


        public ActionResult Post_Budget_Report(string Budget_Year, string Budget_Head, string Report_Type)
        {
            return View();
        }

    }
}