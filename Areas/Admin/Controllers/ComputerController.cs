using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class ComputerController : Controller
    {
        // GET: Admin/Computer
        public ActionResult Details()
        {
           

            return View();
        }

        public ActionResult Add_data()
        {
            return View();
        }

        public ActionResult Submit()
        {
            return View();
        }

    }
}