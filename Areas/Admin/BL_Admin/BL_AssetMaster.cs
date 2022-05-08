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
    public class BL_AssetMaster
    {

        public List<Mod_AssetMaster> Get_Data()
        {

            Mod_AssetMaster BL_data;
            List<Mod_AssetMaster> current_data = new List<Mod_AssetMaster>();

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);



                using (SqlCommand cmd = new SqlCommand("sp_AsetMaster"))
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
                    BL_data = new Mod_AssetMaster();
                    BL_data.Asset_ID = Convert.ToString(dr["ID"]);
                    BL_data.Asset_make = Convert.ToString(dr["Make"]);
                    BL_data.Asset_Model = Convert.ToString(dr["Model"]);
                    BL_data.Asset_Type = Convert.ToString(dr["Aset_Type"]);

                    BL_data.Asset_Status = Convert.ToInt32( (dr["Aset_Status"] == DBNull.Value) ? 0 : dr["Aset_Status"]);

                    


                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_data(Mod_AssetMaster Data)
        {
            int status = 0;
            string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AsetMaster";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", "Add_new");
                cmd.Parameters.Add(sqlP_type);

                SqlParameter Asset_make = new SqlParameter("@Item_Make", Data.Asset_make);
                cmd.Parameters.Add(Asset_make);

                SqlParameter Asset_Model = new SqlParameter("@Item_Model", Data.Asset_Model);
                cmd.Parameters.Add(Asset_Model);

                SqlParameter Asset_Type = new SqlParameter("@Item_Type", Data.Asset_Type);
                cmd.Parameters.Add(Asset_Type);

                con.Open();

                cmd.ExecuteNonQuery();

                status = 1;

            }
            catch (Exception ex) { status = 0; }
            finally { con.Close(); }

            return status;   
        }

    }
}