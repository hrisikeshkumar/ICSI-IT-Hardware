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
    public class BL_Hardware_Amc
    {
        public void Get_Amc_Data(Mod_Amc_Dashboard mod_data)
        {

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_IT_AMC"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_AMC_Warranty_Qty");
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
                 

                    if (dt_Comuter.Rows.Count > 0)
                    {
                        mod_data.PC_AMC = Convert.ToInt32(dt_Comuter.Rows[0]["PC_AMC"]);
                        mod_data.PC_Waranty = Convert.ToInt32(dt_Comuter.Rows[0]["PC_Waranty"]);
                        mod_data.Laptop_AMC = Convert.ToInt32(dt_Comuter.Rows[0]["Laptop_AMC"]);
                        mod_data.Laptop_Waranty = Convert.ToInt32(dt_Comuter.Rows[0]["Laptop_Waranty"]);
                        mod_data.Printer_AMC = Convert.ToInt32(dt_Comuter.Rows[0]["Printer_AMC"]);
                        mod_data.Printer_Waranty = Convert.ToInt32(dt_Comuter.Rows[0]["Printer_Waranty"]);
                        mod_data.Scanner_AMC = Convert.ToInt32(dt_Comuter.Rows[0]["Scanner_AMC"]);
                        mod_data.Scanner_Waranty = Convert.ToInt32(dt_Comuter.Rows[0]["Scanner_Waranty"]);
                        mod_data.Ups_AMC = Convert.ToInt32(dt_Comuter.Rows[0]["Ups_AMC"]);
                        mod_data.Ups_Waranty = Convert.ToInt32(dt_Comuter.Rows[0]["Ups_Waranty"]);
                    }
                }

            }
            catch (Exception ex) { }

        }

        public List<Mod_Warranty_Amc> Find_Warranty_Expired(string Asset_Types)
        {
            List<Mod_Warranty_Amc> Data = new List<Mod_Warranty_Amc>();
            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_IT_AMC"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Find_Warranty_Expired");
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_Asset_Types = new SqlParameter("@Asset_Type", Asset_Types);
                    cmd.Parameters.Add(sqlP_Asset_Types);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_Comuter = dt;
                        }
                    }

                   
                    if (dt_Comuter.Rows.Count > 0)
                    {
                        Mod_Warranty_Amc mod_data;
                        foreach (DataRow Dr in dt_Comuter.Rows)
                        {
                            mod_data = new Mod_Warranty_Amc();

                            mod_data.Item_Id = Convert.ToString(Dr["Item_Id"]);
                            mod_data.Emp_Name = Convert.ToString(Dr["Emp_Name"]);
                            mod_data.Designation = Convert.ToString(Dr["Designation"]);
                            mod_data.Item_SlNo = Convert.ToString(Dr["Item_SlNo"]);
                            mod_data.Asset_Type = Convert.ToString(Dr["Asset_Type"]);
                            mod_data.Warnt_end_DT = Convert.ToDateTime(Dr["Warnt_end_DT"]);

                            Data.Add(mod_data);
                        }
                       
                    }
                }

            }
            catch (Exception ex) { }

            return Data;

        }

    }
}