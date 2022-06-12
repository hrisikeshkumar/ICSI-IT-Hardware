using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.BL_Admin
{
    public class BL_Item_Issue
    {

        public List<Mod_Item_Issue> Get_Item_IssueData()
        {

            Mod_Item_Issue BL_data;
            List<Mod_Item_Issue> current_data = new List<Mod_Item_Issue>();

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_Computer"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List");
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


                foreach (DataRow dr in dt_Comuter.Rows)
                {
                    BL_data = new Mod_Item_Issue();

                    BL_data.Item_Name = Convert.ToString(dr["Asset_Type"]);

                    BL_data.Present_Custady = Convert.ToString(dr["Item_SlNo"]);

                    BL_data.Transfered_Custady = Convert.ToString(dr["Item_Id"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_Item_Issue_data(Mod_Item_Issue Data, string type, string Issued_Id)
        {
            int status = 1;
            string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Computer";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter SqlIssued_Id = new SqlParameter("@Item_Id", Issued_Id);
                    cmd.Parameters.Add(SqlIssued_Id);
                }

                SqlParameter Item_Id = new SqlParameter("@Item_MakeId", Data.Item_Id);
                cmd.Parameters.Add(Item_Id);

                SqlParameter Present_Company = new SqlParameter("@Item_serial_No", Data.Present_Custady);
                cmd.Parameters.Add(Present_Company);

                SqlParameter Shifted_Company = new SqlParameter("@Proc_Date", Data.Transfered_Custady);
                cmd.Parameters.Add(Shifted_Company);

                SqlParameter Shift_date = new SqlParameter("@Warnt_end_DT", Data.Issued_date);
                cmd.Parameters.Add(Shift_date);

                SqlParameter Remarks = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Remarks);

                SqlParameter Asset_Type = new SqlParameter("@Asset_Type", "Item_Issue");
                cmd.Parameters.Add(Asset_Type);

                con.Open();

                cmd.ExecuteNonQuery();

                status = 0;

            }
            catch (Exception ex) { status = 1; }
            finally { con.Close(); }

            return status;
        }

        public Mod_Item_Issue Get_Data_By_ID(string Shift_Unique_Id)
        {
            Mod_Item_Issue Data = new Mod_Item_Issue();

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_Computer"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_Shift_Id = new SqlParameter("@Item_ID", Shift_Unique_Id);
                    cmd.Parameters.Add(sqlP_Shift_Id);

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
                    Data.Item_Issue_Id = Convert.ToString(dt_Comuter.Rows[0]["Item_Id"]);
                    Data.Item_SerialNo = Convert.ToString(dt_Comuter.Rows[0]["Item_MakeId"]);
                    Data.Item_Name = Convert.ToString(dt_Comuter.Rows[0]["Item_SlNo"]);
                    Data.Item_Transfered_Location = Convert.ToString(dt_Comuter.Rows[0]["Item_MakeId"]);
                    Data.Present_Custady = Convert.ToString(dt_Comuter.Rows[0]["Item_MakeId"]);
                    Data.Transfered_Custady = Convert.ToString(dt_Comuter.Rows[0]["Item_MakeId"]);
                    Data.Issued_date = Convert.ToDateTime(dt_Comuter.Rows[0]["Proc_Date"]).Date;
                    Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);

                }

            }
            catch (Exception ex) { }

            return Data;
        }
    }
}