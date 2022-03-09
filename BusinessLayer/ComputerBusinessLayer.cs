using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IT_Hardware_Aug2021.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace IT_Hardware_Aug2021.BusinessLayer
{
    public class ComputerBusinessLayer
    {
        public List<DesktopPC> Get_Desktop_list()
        {

            List<DesktopPC> Pc_List = new List<DesktopPC>();

            try
            {
                DesktopPC Pc;
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);

                using (SqlCommand cmd = new SqlCommand("sp_Computer"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Lst_Desk");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add(sqlP_type);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dt_Comuter =dt;
                        }
                    }
                }

                foreach (DataRow dr in dt_Comuter.Rows)
                {
                    Pc = new DesktopPC();
                    Pc.Item_Type = Convert.ToString( dr["Item_Type"]);
                    Pc.Item_serial_No = Convert.ToString(dr["Item_serial_No"]);
                    Pc.Wrnty_status = Convert.ToString( dr["Wrnty_status"]);
                    Pc.Proc_date = Convert.ToDateTime(dr["Proc_date"]);
                    Pc.Warnt_strt_dt = Convert.ToDateTime(dr["Warnt_strt_dt"]);
                    Pc.Warnt_end_dt = Convert.ToDateTime( dr["Warnt_end_dt"]);

                    Pc_List.Add(Pc);
                }

            }
            catch (Exception ex) { }

            return Pc_List;
        }

        public int Save_Item(DesktopPC Get_Pc , String type )
        {
            int var_return = 0;

            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Computer"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;

                        SqlParameter sqlP_type = new SqlParameter("@Type", type);
                        SqlParameter Item_serial_No = new SqlParameter("@Item_serial_No", Get_Pc.Item_serial_No);
                        SqlParameter Emp_code = new SqlParameter("@Emp_code", Get_Pc.Emp_code);
                        SqlParameter Emp_Name = new SqlParameter("@Emp_Name", Get_Pc.Emp_Name);
                        SqlParameter Wrnty_status = new SqlParameter("@Wrnty_status", Get_Pc.Wrnty_status);
                        SqlParameter Amc_status = new SqlParameter("@Amc_status", Get_Pc.Amc_status);
                        SqlParameter Dept = new SqlParameter("@Dept", Get_Pc.Dept);

                        //if (Get_Pc.Proc_date.ToString() ==string.Empty || (Convert.ToDateTime(Get_Pc.Proc_date)).Year <= 1900)
                        //{
                        //    Get_Pc.Proc_date = ;
                        //}

                        SqlParameter Proc_date = new SqlParameter("@Proc_date", Get_Pc.Proc_date);
                        SqlParameter Warnt_end_dt = new SqlParameter("@Warnt_end_dt", Get_Pc.Warnt_end_dt);
                        SqlParameter Amc_strt_dt = new SqlParameter("@Amc_strt_dt", Get_Pc.Amc_strt_dt);
                        SqlParameter Amc_end_dt = new SqlParameter("@Amc_end_dt", Get_Pc.Amc_end_dt);

                        cmd.Parameters.Add(sqlP_type);
                        cmd.Parameters.Add(Item_serial_No);
                        cmd.Parameters.Add(Emp_code);
                        cmd.Parameters.Add(Emp_Name);
                        cmd.Parameters.Add(Wrnty_status);
                        cmd.Parameters.Add(Amc_status);
                        cmd.Parameters.Add(Dept);
                        cmd.Parameters.Add(Proc_date);
                        cmd.Parameters.Add(Warnt_end_dt);
                        cmd.Parameters.Add(Amc_strt_dt);
                        cmd.Parameters.Add(Amc_end_dt);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        var_return = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                var_return = 1;
            }
            finally
            {
                
            }
            
            return var_return;

        }

    }


}