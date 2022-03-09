using System.Web.Mvc;

namespace IT_Asset.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }


       

        public override void RegisterArea(AreaRegistrationContext context) 
        {

          

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller= "Admin_Dashboard", action = "Admin_Dashboard", id = UrlParameter.Optional },
                new string[] { "IT_Hardware_Aug2021.Areas.Admin.Controllers" });
        }
    }
}