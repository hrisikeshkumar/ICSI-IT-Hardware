﻿
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
    public class BL_Budget_Head
    {

        public List<Mod_Budget> Get_BudgetData()
        {

            Mod_Budget BL_data;
            List<Mod_Budget> current_data = new List<Mod_Budget>();

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_Budget_Head"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_List");
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
                    BL_data = new Mod_Budget();

                    BL_data.Budget_Head_Id = Convert.ToString(dr["Asset_Type"]);

                    BL_data.Budget_Name = Convert.ToString(dr["Item_SlNo"]);

                    BL_data.Total_Budget_Amount = Convert.ToString(dr["Item_Id"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_Budget_data(Mod_Budget Data, string type, string Budget_Head_ID)
        {
            int status = 1;
            string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Budget_Head";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter Asset_Id = new SqlParameter("@Budget_Head_Id", Budget_Head_ID);
                    cmd.Parameters.Add(Asset_Id);
                }

                SqlParameter Asset_Make_Id = new SqlParameter("@Budget_Year", Data.Budget_Year);
                cmd.Parameters.Add(Asset_Make_Id);

                SqlParameter Asset_SL_No = new SqlParameter("@Budget_Name", Data.Budget_Name);
                cmd.Parameters.Add(Asset_SL_No);

                SqlParameter Proc_Date = new SqlParameter("@Total_Budget_Amount", Data.Total_Budget_Amount);
                cmd.Parameters.Add(Proc_Date);

                SqlParameter Warnt_end_dt = new SqlParameter("@Remarks", Data.Remarks);
                cmd.Parameters.Add(Warnt_end_dt);

                con.Open();

                status = cmd.ExecuteNonQuery();



            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public Mod_Budget Get_Data_By_ID(Mod_Budget Data, string Budget_Head_Id)
        {


            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_Budget_Head"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_Asset_ID = new SqlParameter("@Budget_Head_Id", Budget_Head_Id);
                    cmd.Parameters.Add(sqlP_Asset_ID);

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
                    Data.Budget_Head_Id = Convert.ToString(dt_Comuter.Rows[0]["Budget_Head_Id"]);
                    Data.Budget_Year = Convert.ToString(dt_Comuter.Rows[0]["Budget_Year"]);
                    Data.Budget_Name = Convert.ToString(dt_Comuter.Rows[0]["Budget_Name"]);
                    Data.Total_Budget_Amount = Convert.ToString(dt_Comuter.Rows[0]["Total_Budget_Amount"]);
                    Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);
                }

            }
            catch (Exception ex) { }

            return Data;
        }


    }
}