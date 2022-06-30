using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using IT_Hardware_Aug2021.Areas.Admin.Models;
using System.Web.Mvc;


namespace IT_Hardware_Aug2021.Areas.Admin.BL_Admin
{
    public class BL_SLA
    {


        public List<Mod_SLA> Get_SLAData()
        {

            Mod_SLA BL_data;
            List<Mod_SLA> current_data = new List<Mod_SLA>();

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_SLA_List"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List");
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_Asset_Type = new SqlParameter("@Asset_Type", "SLA");
                    cmd.Parameters.Add(sqlP_Asset_Type);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_Comuter = dt;
                        }
                    }
                }


                foreach (DataRow dr in dt_Comuter.Rows)
                {
                    BL_data = new Mod_SLA();

                    BL_data.SLA_Id = Convert.ToString(dr["Unique_Id"]);

                    BL_data.Vendor_id = Convert.ToString(dr["Vendor_ID"]);

                    BL_data.Expiry_DT = Convert.ToDateTime(dr["Service_ST_DT"]);

                    BL_data.Service_Type_Short = Convert.ToString(dr["Service_Type_Short"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_SLA_data(Mod_SLA Data, string type, string SLA_ID)
        {
            int status = 1;
            string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_SLA_List";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter Asset_Id = new SqlParameter("@Unique_Id", SLA_ID );
                    cmd.Parameters.Add(Asset_Id);
                }

                SqlParameter Vendor_ID = new SqlParameter("@Vendor_ID", Data.Vendor_id);
                cmd.Parameters.Add(Vendor_ID);

                SqlParameter Service_Type_Short = new SqlParameter("@Service_Type_Short", Data.Service_Type_Short);
                cmd.Parameters.Add(Service_Type_Short);

                SqlParameter Service_Type_Details = new SqlParameter("@Service_Type_Short", Data.Service_Type_Details);
                cmd.Parameters.Add(Service_Type_Details);

                SqlParameter PO_Copy = new SqlParameter("@PO_Copy", Data.PO_Copy);
                cmd.Parameters.Add(PO_Copy);

                SqlParameter SLA_Copy = new SqlParameter("@SLA_Copy", Data.SLA_Copy);
                cmd.Parameters.Add(SLA_Copy);

                SqlParameter Approval_Copy = new SqlParameter("@Approval_Copy", Data.Approval_Copy);
                cmd.Parameters.Add(Approval_Copy);

                SqlParameter Service_ST_DT = new SqlParameter("@Service_ST_DT", Data.Service_ST_DT);
                cmd.Parameters.Add(Service_ST_DT);

                SqlParameter Expiry_DT = new SqlParameter("@Expiry_DT", Data.Expiry_DT);
                cmd.Parameters.Add(Expiry_DT);


                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);

                con.Open();

                status = cmd.ExecuteNonQuery();



            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public void Get_Data_By_ID(Mod_SLA Data, string Asset_Id)
        {


            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_SLA_List"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_Comuter = dt;
                        }
                    }
                }

                if (dt_Comuter.Rows.Count > 0)
                {
                    Data.SLA_Id = Convert.ToString(dt_Comuter.Rows[0]["Unique_Id"]);
                    Data.Vendor_id = Convert.ToString(dt_Comuter.Rows[0]["Vendor_id"]);
                    Data.Service_Type_Short = Convert.ToString(dt_Comuter.Rows[0]["Service_Type_Short"]);
                    Data.Service_Type_Details = Convert.ToString(dt_Comuter.Rows[0]["Service_Type_Details"]);
                    Data.PO_Copy = Convert.ToString(dt_Comuter.Rows[0]["PO_Copy"]);
                    Data.Approval_Copy = Convert.ToString(dt_Comuter.Rows[0]["Approval_Copy"]);
                    Data.SLA_Copy = Convert.ToString(dt_Comuter.Rows[0]["SLA_Copy"]);
                    Data.Service_ST_DT = Convert.ToDateTime(dt_Comuter.Rows[0]["Service_ST_DT"]).Date;
                    Data.Expiry_DT = Convert.ToDateTime(dt_Comuter.Rows[0]["Expiry_DT"]).Date;
                    Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);

                }

            }
            catch (Exception ex) { }

        }




        public List<SelectListItem> Vendor_List()
        {

            List<SelectListItem> List_Item = new List<SelectListItem>();

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("Vendor_List"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                   
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_Comuter = dt;
                        }
                    }
                }

                foreach (DataRow dr in dt_Comuter.Rows)
                {
                    SelectListItem Listdata = new SelectListItem();
                    Listdata.Value = Convert.ToString(dr["Vendor_ID"]);
                    Listdata.Text = Convert.ToString(dr["Vendor_name"]);

                    List_Item.Add(Listdata);
                }

            }
            catch (Exception ex) { }

            return List_Item;
        }





    }
}