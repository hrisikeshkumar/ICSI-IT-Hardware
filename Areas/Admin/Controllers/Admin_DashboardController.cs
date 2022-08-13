using IT_Asset.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IT_Hardware_Aug2021.User_Role;
using System.Web.Mvc.Filters;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Admin_DashboardController : Controller
    {

        //[Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Admin_Dashboard()
        {

            BL_Admin_DashB B_Layer = new BL_Admin_DashB();

            Mod_Admin_dashB mod_Data = new Mod_Admin_dashB();

            mod_Data.List_Proposal = B_Layer.Get_List_Proposal();

            mod_Data.List_Bill_Process = B_Layer.Get_List_Bills();


            return View("~/Areas/Admin/Views/Admin_Dashboard/Admin_Dashboard.cshtml", mod_Data);

           
        }


        public JsonResult Get_Proposal_Detail_for_Modal( string Proposal_Id)
        {
            BL_Admin_DashB B_Layer = new BL_Admin_DashB();

            Proposal_details detail_Data = new Proposal_details();

            Mod_Admin_dashB mod_Data = new Mod_Admin_dashB();

            mod_Data.Prop_detail = detail_Data;

            B_Layer.Get_Proposal_By_Id( mod_Data, Proposal_Id);


            return Json(mod_Data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Edit_proposal(Mod_Admin_dashB Proposal)
        {

            BL_Admin_DashB B_Layer = new BL_Admin_DashB();

            Proposal_details detail_Data = new Proposal_details();

            Mod_Admin_dashB mod_Data = new Mod_Admin_dashB();

            mod_Data.Prop_detail = detail_Data;

            B_Layer.Get_Proposal_By_Id(mod_Data, Proposal.Prop_detail.Proposal_Id);

            return View("~/Areas/Admin/Views/Admin_Dashboard/Edit_Proposal.cshtml", mod_Data.Prop_detail);

        }


        public ActionResult Update_proposal(Proposal_details Proposal_data)
        {

            int status = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    BL_Admin_DashB B_Layer = new BL_Admin_DashB();

                    status = B_Layer.Update_proposal(Proposal_data);

                    if (status > 0)
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

            return RedirectToAction("Admin_Dashboard", "Admin_Dashboard");
        }


        //protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        //{
        //    if (filterContext.HttpContext.Request.IsAuthenticated)
        //    {

        //        if (filterContext.Result is HttpUnauthorizedResult)
        //        {
        //            filterContext.Result = new RedirectResult("~/Authorization/AccessDedied");
        //        }
        //    }
        //    else
        //    {
        //        filterContext.Result = new RedirectResult("~/Log_In/Log_In");
        //    }
        //}

    }
}