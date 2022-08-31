using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class VendorController : Controller
    {
        [Authorize(Roles = "SU, Admin, Manager, InventoryManager")]
        public ActionResult Vendor_Details()
        {
            BL_Vendor com = new BL_Vendor();

            List<Mod_Vendor> pc_List = com.Get_VendorData();

            return View("~/Areas/Admin/Views/Vendor/Vendor_Details.cshtml", pc_List);
        }


        [Authorize(Roles = "SU, Admin, Manager")]
        [HttpGet]
        public ActionResult Vendor_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            return View("~/Areas/Admin/Views/Vendor/Vendor_Create_Item.cshtml");

        }


        [Authorize(Roles = "SU, Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vendor_Create_Post(Mod_Vendor Get_Data)
        {
            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                string Vendor_Id = string.Empty;
                if (ModelState.IsValid)
                {
                    BL_Vendor save_data = new BL_Vendor();
                    int status = save_data.Save_Vendor_data(Get_Data, "Add_new", "", out Vendor_Id);

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data save successfully");



                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            //HttpPostedFile httpPostedFile = Request.Files;

                            var postedFile = Request.Files[i];

                            if (postedFile != null)
                            {

                                byte[] bytes;
                                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                                {
                                    bytes = br.ReadBytes(postedFile.ContentLength);
                                }
                                string constr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                                using (SqlConnection con = new SqlConnection(constr))
                                {
                                    string query = "INSERT INTO File_table(File_Id,  File_Table, File_Ref_Id, File_Name, ContentType, File_Data) VALUES ( dbo.Get_Unique_File_Id(), 'Vendor', @Ref_Id,   @Name, @ContentType, @Data)";
                                    using (SqlCommand cmd = new SqlCommand(query))
                                    {
                                        cmd.Connection = con;
                                        cmd.Parameters.AddWithValue("@Name", Path.GetFileName(postedFile.FileName));
                                        cmd.Parameters.AddWithValue("@ContentType", postedFile.ContentType);
                                        cmd.Parameters.AddWithValue("@Data", bytes);
                                        cmd.Parameters.AddWithValue("@Ref_Id", Vendor_Id);
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }


                            }
                        }


                    }
                    else
                    {

                        TempData["Message"] = String.Format("Data is not saved");
                    }
                }
                else
                {
                    TempData["Message"] = String.Format("Required Data are not Provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Vendor_Create_Item", "Vendor");
        }


        [Authorize(Roles = "SU, Admin, Manager")]
        public ActionResult Edit_Vendor(string id)
        {
            BL_Vendor Md_Com = new BL_Vendor();
            Mod_Vendor data = Md_Com.Get_Data_By_ID(id);

            data.File_List = GetFiles_By_Id(id);


            return View("~/Areas/Admin/Views/Vendor/Edit_Vendor.cshtml", data);
        }



        [Authorize(Roles = "SU, Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update_Vendor(Mod_Vendor Get_Data)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_Vendor Md_Asset = new BL_Vendor();

                    string out_param = string.Empty;

                    status = Md_Asset.Save_Vendor_data(Get_Data, "Update", Get_Data.Vendor_id, out out_param);

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data have saved successfully");
                    }
                    else
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                }
                else
                {
                    TempData["Message"] = String.Format("Required Data are not Provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("Vendor_Details", "Vendor");
        }


        [Authorize(Roles = "SU, Admin, Manager")]
        public ActionResult Delete_Vendor(Mod_Vendor Get_Data, string id)
        {
            int status = 0;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {


                    BL_Vendor Md_Asset = new BL_Vendor();

                    string out_param = string.Empty;

                    status = Md_Asset.Save_Vendor_data(Get_Data, "Delete", id,  out out_param);

                    if (status >0)
                    {
                        TempData["Message"] = String.Format("Data saved successfully");
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

            return RedirectToAction("Vendor_Details", "Vendor");
        }


        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {

                if (filterContext.Result is HttpUnauthorizedResult)
                {
                    filterContext.Result = new RedirectResult("~/Authorization/AccessDedied");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Log_In/Log_In");
            }
        }



        [HttpPost]
        public JsonResult FiliUpload()
        {

            HttpPostedFileBase postedFile = Request.Files[0];

            string SLA_Id = Request.Form["SLA_Id"].ToString();

            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }
            string constr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "INSERT INTO File_table(File_Id,  File_Table, File_Ref_Id, File_Name, ContentType, File_Data) VALUES ( dbo.Get_Unique_File_Id(), 'Vendor', @Ref_Id,   @Name, @ContentType, @Data)";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Name", Path.GetFileName(postedFile.FileName));
                    cmd.Parameters.AddWithValue("@ContentType", postedFile.ContentType);
                    cmd.Parameters.AddWithValue("@Data", bytes);
                    cmd.Parameters.AddWithValue("@Ref_Id", SLA_Id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }


            return Json(GetFiles_By_Id(SLA_Id), JsonRequestBehavior.AllowGet);

        }



        private static List<FileModel> GetFiles_By_Id(string SLA_Id)
        {
            List<FileModel> files = new List<FileModel>();
            string constr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT File_Id, File_Name FROM File_table where  LTRIM(RTRIM(File_table))= 'Vendor' and  LTRIM(RTRIM(File_Ref_Id))=LTRIM(RTRIM('" + SLA_Id + "')) "))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            files.Add(new FileModel
                            {
                                File_Id = Convert.ToString(sdr["File_Id"]),
                                File_Name = Convert.ToString(sdr["File_Name"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return files;
        }



        public JsonResult DeleteFile(string FileId, string RefId)
        {

            string constr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "delete from File_table where LTRIM(RTRIM(File_Id)) =LTRIM(RTRIM(@File_Id))";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@File_Id", FileId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }


            return Json(GetFiles_By_Id(RefId), JsonRequestBehavior.AllowGet);

        }



        public FileResult Download(string fileId)
        {


            byte[] bytes;
            string fileName, contentType;
            string constr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select File_Id, File_table, File_Name, ContentType, File_Data from File_table WHERE LTRIM(RTRIM(File_Id))= LTRIM(RTRIM(@Id))";
                    cmd.Parameters.AddWithValue("@Id", fileId);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["File_Data"];
                        contentType = sdr["ContentType"].ToString();
                        fileName = sdr["File_Name"].ToString();
                    }
                    con.Close();
                }
            }


            return File(bytes, contentType, fileName);
        }













    }
}