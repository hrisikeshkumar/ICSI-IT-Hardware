using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Asset.Controllers
{
    public class Log_InController : Controller
    {
        // GET: Log_In
        [HttpGet]
        public ActionResult Log_In()
        {
            return View("Log_In");
        }

        [HttpPost]
        [ActionName("Log_In")]
        public ActionResult Log_In_Submit()
        {
            string UserName = Request["txt_UserName"].ToString();
            string Password = Request["txt_Password"].ToString();

            if (UserName != string.Empty && Password != string.Empty)
            {
                return RedirectToAction("Admin_Dashboard", "Admin_Dashboard", new { area = "Admin" });
            }
            else {
                ViewBag.Message = "UserName or password is wrong";
                return View("Log_In");
            }
            
        }
    }
}


