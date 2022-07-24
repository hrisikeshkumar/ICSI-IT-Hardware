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
    public class BL_UserRole
    {
        public List<Mod_UserRole> Get_UserRoleData()
        {

            Mod_UserRole BL_Mod_Role = new Mod_UserRole();
            List<Mod_UserRole> current_data = new List<Mod_UserRole>();

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_UserRole"))
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
                int Prev_ID=9999;



                foreach (DataRow dr in dt_Comuter.Rows)
                {
                    

                    if (Prev_ID != Convert.ToInt32(dr["UserID"]))
                    {
                        BL_Mod_Role = new Mod_UserRole();
                        BL_Mod_Role.User_ID = Convert.ToString(dr["UserID"]);
                        BL_Mod_Role.User_fullname = Convert.ToString(dr["fullname"]);
                        BL_Mod_Role.SU_Role = false;
                        BL_Mod_Role.Admin_Role = false;
                        BL_Mod_Role.Manager_Role = false;
                        BL_Mod_Role.InventoryManager_Role = false;
                        BL_Mod_Role.FmsEngineer_Role = false;
                        BL_Mod_Role.ServerEngineer_Role = false;

                    }


                    
                        if (Convert.ToString(dr["RoleName"]).Trim() == "SU")
                        {
                            BL_Mod_Role.SU_Role = true;
                        }
                        if (Convert.ToString(dr["RoleName"]) == "Admin")
                        {
                            BL_Mod_Role.Admin_Role = true;
                        }
                        if (Convert.ToString(dr["RoleName"]) == "Manager")
                        {
                            BL_Mod_Role.Manager_Role = true;
                        }
                        if (Convert.ToString(dr["RoleName"]) == "InventoryManager")
                        {
                            BL_Mod_Role.InventoryManager_Role = true;
                        }
                        if (Convert.ToString(dr["RoleName"]) == "FmsEngineer")
                        {
                            BL_Mod_Role.FmsEngineer_Role = true;
                        }
                        if (Convert.ToString(dr["RoleName"]) == "ServerEngineer")
                        {
                            BL_Mod_Role.ServerEngineer_Role = true;
                        }
                    

                    if (Prev_ID != Convert.ToInt32(dr["UserID"]))
                    {
                        current_data.Add(BL_Mod_Role);
                    }
                    Prev_ID = Convert.ToInt32(dr["UserID"]);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public int Save_UserRole_data(Mod_UserRole Data, string type, string UserRole_ID)
        {
            int status = 1;
            string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_UserRole";

                cmd.Connection = con;

                SqlParameter sqlP_type = new SqlParameter("@Type", type);
                cmd.Parameters.Add(sqlP_type);

                if (type == "Update" || type == "Delete")
                {
                    SqlParameter Sql_UserRole_ID = new SqlParameter("@UserRole_Id", UserRole_ID);
                    cmd.Parameters.Add(Sql_UserRole_ID);
                }


                SqlParameter User_Role = new SqlParameter("@User_Role", Data.User_Role);
                cmd.Parameters.Add(User_Role);


                con.Open();

                status = cmd.ExecuteNonQuery();



            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }


    }
}