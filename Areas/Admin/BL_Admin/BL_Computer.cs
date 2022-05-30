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
    public class BL_Computer
    {

        public List<Mod_Computer> Get_Data()
        {

            Mod_Computer BL_data;
            List<Mod_Computer> current_data = new List<Mod_Computer>();

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
                    BL_data = new Mod_Computer();
                    
                    BL_data.Item_Model = Convert.ToString(dr["Model"]);
                    BL_data.Amc_Vndr_Name = Convert.ToString(dr["Aset_Type"]);

                  

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }


    }
}