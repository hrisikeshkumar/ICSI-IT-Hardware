using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using IT_Hardware_Aug2021.Models;


namespace IT_Hardware_Aug2021.BAL
{
    public class BL_LogIn
    {

        public Boolean User_Authentication(Mod_LogIn Mod)
        {
            int Value = 0;
            Boolean Auth = false;
            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_UserAuth"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_User_By_ID");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_UserName = new SqlParameter("@UserName", Mod.UserName);
                    cmd.Parameters.Add(sqlP_UserName);

                    SqlParameter sqlP_Password = new SqlParameter("@Password", Mod.Password);
                    cmd.Parameters.Add(sqlP_Password);

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

                Value = dt_Comuter.Rows.Count;


                if (Value > 0)
                {
                    Auth = true;
                    Mod.UserId = Convert.ToString(dt_Comuter.Rows[0]["UserID"]);
                    Mod.UserName = Convert.ToString(dt_Comuter.Rows[0]["User_Email"]);
                    
                }

            }
            catch (Exception ex) { }

            return Auth;
        }


        public string[] User_Role(string User_name)
        {
            
            string[] User_Role = new string[0];

          
            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_UserAuth"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_User_Roles");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    SqlParameter sqlP_UserName = new SqlParameter("@UserName", User_name);
                    cmd.Parameters.Add(sqlP_UserName);

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

                User_Role = new string[dt_Comuter.Rows.Count];


                for (int i=0; i< dt_Comuter.Rows.Count; i++)
                {
                    User_Role[i] = Convert.ToString(dt_Comuter.Rows[i]["RoleName"]);
                }
        

            }
            catch (Exception ex) { }

            return User_Role;
        }


    }

}