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
        public IEnumerable<DesktopPC> Get_Desktop_list()
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

    }


}