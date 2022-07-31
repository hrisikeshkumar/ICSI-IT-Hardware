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


    }
}