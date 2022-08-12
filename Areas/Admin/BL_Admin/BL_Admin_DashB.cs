using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using IT_Asset.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.BL_Admin
{
    public class BL_Admin_DashB
    {
        public List<mod_Admin_Propsal_List> Get_List_Proposal()
        {

            mod_Admin_Propsal_List BL_data;
            List<mod_Admin_Propsal_List> current_data = new List<mod_Admin_Propsal_List>();

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_IT_Proposal"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Proposal_List");
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
                    BL_data = new mod_Admin_Propsal_List();

                    BL_data.Proposal_Id = Convert.ToString(dr["Proposal_Id"]);

                    BL_data.Utilization_Details = Convert.ToString(dr["Utilization_Details"]);

                    BL_data.IT_Initiate_Date = Convert.ToDateTime(dr["IT_Initiate_Date"]);

                    BL_data.Status = Convert.ToString(dr["Completed_Status"]);

                    current_data.Add(BL_data);
                }

            }
            catch (Exception ex) { }

            return current_data;
        }

        public void Get_Proposal_By_Id(Mod_Admin_dashB BL_data , string Proposal_Id)
        {

            try
            {
                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_IT_Proposal"))
                {
                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Proposal_by_Id");
                    cmd.Parameters.Add(sqlP_type);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;


                    SqlParameter sql_Proposal_Id = new SqlParameter("@Proposal_Id", Proposal_Id);
                    cmd.Parameters.Add(sql_Proposal_Id);


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


               if(dt_Comuter.Rows.Count>0)
                { 

                    BL_data.Proposal_Id = Convert.ToString(dt_Comuter.Rows[0]["Proposal_Id"]);

                    BL_data.Utilization_Details = Convert.ToString(dt_Comuter.Rows[0]["Utilization_Details"]);

                    BL_data.Dte_IT_Remarks = Convert.ToString(dt_Comuter.Rows[0]["Dte_IT_Remarks"]);

                    BL_data.FA_Remarks = Convert.ToString(dt_Comuter.Rows[0]["FA_Remarks"]);

                    BL_data.Sec_Office_Remarks = Convert.ToString(dt_Comuter.Rows[0]["Sec_Office_Remarks"]);

                    BL_data.Purchase_Remarks = Convert.ToString(dt_Comuter.Rows[0]["Purchase_Remarks"]);

                    BL_data.Other_Dept_Remarks = Convert.ToString(dt_Comuter.Rows[0]["Other_Dept_Remarks"]);


                }
            }
            catch (Exception ex) { }

           
        }



    }
}