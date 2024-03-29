﻿using IT_Hardware_Aug2021.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IT_Hardware_Aug2021.Areas.Admin.BL_Admin
{
    public class BL_StockReport
    {
        public List<Mod_StockReport> Get_CompData(string Item_Type)
        {

            Mod_StockReport BL_data;
            List<Mod_StockReport> current_data = new List<Mod_StockReport>( );

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_StockReport"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter Asset_type = new SqlParameter("@ItemType", Item_Type.Trim());
                    cmd.Parameters.Add(Asset_type);

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Stock_List");
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
                    BL_data = new Mod_StockReport();

                    BL_data.Emp_Name = Convert.ToString(dr["Emp_Name"]);

                    BL_data.Emp_Designation = Convert.ToString(dr["Designation_name"]);

                    BL_data.Emp_Location = Convert.ToString(dr["Emp_Location"]);

                    BL_data.Emp_Dept = Convert.ToString(dr["Dept_name"]);

                    BL_data.Item_Type = Convert.ToString(dr["Asset_Type"]);

                    BL_data.Item_Make = Convert.ToString(dr["Make"]);

                    BL_data.Item_Model = Convert.ToString(dr["model"]);

                    BL_data.Item_SlNo = Convert.ToString(dr["Item_SlNo"]);

                    BL_data.Item_Proc_date = Convert.ToDateTime(dr["Proc_Date"]);

                    BL_data.Warranty_Exp_date = Convert.ToDateTime(dr["Warnt_end_DT"]);

                    BL_data.Obsolete = ( Convert.ToInt32(dr["Obsolete_Item"]) ==0)?  "": "Yes";

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

    }
}