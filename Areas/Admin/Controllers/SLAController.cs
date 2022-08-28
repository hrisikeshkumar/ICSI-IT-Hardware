using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;
using System.Web.Http;
using System.Buffers.Text;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class SLAController : Controller
    {

        //[Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult SLA_Details()
        {
            BL_SLA com = new BL_SLA();

            List<Mod_SLA> SLA_List = com.Get_SLAData();

            return View("~/Areas/Admin/Views/SLA/SLA_Details.cshtml", SLA_List);
        }


        //[Authorize(Roles = "SU, Admin, Manager")]
        [HttpGet]
        public ActionResult SLA_Create_Item(string Message)
        {
            ViewBag.Message = Message;

            BL_SLA BL_data = new BL_SLA();

            Mod_SLA Mod_data = new Mod_SLA();
            
            Mod_data.Vendor_List = BL_data.Vendor_List();


            return View("~/Areas/Admin/Views/SLA/SLA_Create_Item.cshtml", Mod_data);

        }


        //[Authorize(Roles = "SU, Admin, Manager")]
        [HttpPost]
        public ActionResult SLA_CreateItem_Post(Mod_SLA Get_Data)
        {

            string Message = "";
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                string SLA_Id = string.Empty;
                if (ModelState.IsValid)
                {
                    BL_SLA save_data = new BL_SLA();
                    int status = save_data.Save_SLA_data(Get_Data, "Add_new", "", out SLA_Id);

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data saved successfully");

                        //var httpContext = HttpContext.Request.Files;

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
                                    string query = "INSERT INTO File_table(File_Id,  File_Table, File_Ref_Id, File_Name, ContentType, File_Data) VALUES ( dbo.Get_Unique_File_Id(), 'SLA', @Ref_Id,   @Name, @ContentType, @Data)";
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
                    TempData["Message"] = String.Format("Required data are not provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("SLA_Create_Item", "SLA");
        }





        //[Authorize(Roles = "SU, Admin, Manager")]
        public ActionResult Edit_SLA(string id)
        {
            BL_SLA BL_data = new BL_SLA();

            Mod_SLA Mod_data = new Mod_SLA();
            BL_data.Get_Data_By_ID(Mod_data, id);

            Mod_data.Vendor_List = BL_data.Vendor_List();

            Mod_data.File_List = GetFiles_By_Id(Mod_data.SLA_Id);


            return View("~/Areas/Admin/Views/SLA/Edit_SLA.cshtml", Mod_data);
        }


        //[Authorize(Roles = "SU, Admin, Manager")]
        public ActionResult Update_SLA(Mod_SLA Get_Data, string Item_id)
        {
            int status = 0;
            string SLA_Id_I = string.Empty;
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    BL_SLA Md_Asset = new BL_SLA();

                    status = Md_Asset.Save_SLA_data(Get_Data, "Update", Item_id,  out SLA_Id_I );

                    if (status > 0)
                    {
                        TempData["Message"] = String.Format("Data saved successfully");
                    }
                    else
                    {
                        TempData["Message"] = String.Format("Data is not saved");
                    }
                }
                else
                {
                    TempData["Message"] = String.Format("Required data are not provided");
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = string.Format("ShowFailure();");

            }

            return RedirectToAction("SLA_Details", "SLA");
        }


        //[Authorize(Roles = "SU, Admin, Manager")]
        public ActionResult Delete_SLA(Mod_SLA Get_Data, string id)
        {
           
            try
            {
                Get_Data.Create_usr_id = HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    string SLA_Id = string.Empty;

                    BL_SLA Md_Asset = new BL_SLA();

                    int status = Md_Asset.Save_SLA_data(Get_Data, "Delete", id, out SLA_Id);

                    if (status>0)
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

            return RedirectToAction("SLA_Details", "SLA");
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




        //-----------------------------------  File Function ---------------------------------------------



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
                string query = "INSERT INTO File_table(File_Id,  File_Table, File_Ref_Id, File_Name, ContentType, File_Data) VALUES ( dbo.Get_Unique_File_Id(), 'SLA', @Ref_Id,   @Name, @ContentType, @Data)";
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



        //private static List<FileModel> GetFiles()
        //{
        //    List<FileModel> files = new List<FileModel>();
        //    string constr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SELECT File_Id, File_Name FROM File_table"))
        //        {
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    files.Add(new FileModel
        //                    {
        //                        File_Id = Convert.ToString(sdr["File_Id"]),
        //                        File_Name = Convert.ToString(sdr["File_Name"])
        //                    }) ;
        //                }
        //            }
        //            con.Close();
        //        }
        //    }
        //    return files;
        //}



        private static List<FileModel> GetFiles_By_Id(string SLA_Id)
        {
            List<FileModel> files = new List<FileModel>();
            string constr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT File_Id, File_Name FROM File_table where  LTRIM(RTRIM(File_table))= 'SLA' and  LTRIM(RTRIM(File_Ref_Id))=LTRIM(RTRIM('" + SLA_Id + "')) "))
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