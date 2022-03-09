using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Move_ItemsController : Controller
    {
        // GET: Admin/Move_Items
        [HttpGet]
        public ActionResult Item_Movement()
        {
            
            return View("~/Areas/Admin/Views/Move_Items/Item_Movement.cshtml");
        }

        [HttpPost]
        public ActionResult Item_Movement_Submit()
        {

            return View("~/Areas/Admin/Views/Move_Items/Item_Movement.cshtml");
        }
    }
}