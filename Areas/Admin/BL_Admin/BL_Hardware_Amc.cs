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

        public List<Mod_List_Warranty_Amc> Find_Warranty_Expired(Mod_Amc_Dtl mod_data1 ,string Asset_Types)
        {
            List<Mod_List_Warranty_Amc> Data = new List<Mod_List_Warranty_Amc>();
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

                    SqlParameter Warnty_Check_Date = new SqlParameter("@Warnt_End_Dt", mod_data1.Warnty_Check_Date);
                    cmd.Parameters.Add(Warnty_Check_Date);

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
                        Mod_List_Warranty_Amc mod_data;
                        foreach (DataRow Dr in dt_Comuter.Rows)
                        {
                            mod_data = new Mod_List_Warranty_Amc();

                            mod_data.Item_Id = Convert.ToString(Dr["Item_Id"]);
                            mod_data.Emp_Name = Convert.ToString(Dr["Emp_Name"]);
                            mod_data.Designation = Convert.ToString(Dr["Designation"]);
                            mod_data.Item_SlNo = Convert.ToString(Dr["Item_SlNo"]);                           
                            mod_data.Warnt_end_DT = Convert.ToDateTime(Dr["Warnt_end_DT"]);

                            Data.Add(mod_data);
                        }
                       
                    }
                }

            }
            catch (Exception ex) { }

            return Data;

        }

        public List<mod_AMC_Warranty_List> Get_Item_in_AMC_or_Warranty(string List_Type, string Asset_Types)
        {
            List<mod_AMC_Warranty_List> Data = new List<mod_AMC_Warranty_List>();
            try
            {
                if (List_Type == "AMC")
                {
                    List_Type = "Get_Item_in_AMC";
                }
                else
                {
                    List_Type = "Get_Item_in_Warranty";
                }


                DataTable dt_Comuter;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_IT_AMC"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", List_Type);
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
                        mod_AMC_Warranty_List mod_data;
                        foreach (DataRow Dr in dt_Comuter.Rows)
                        {
                            mod_data = new mod_AMC_Warranty_List();

                            mod_data.Emp_Name = Convert.ToString(Dr["Emp_Name"]);
                            mod_data.Designation = Convert.ToString(Dr["Designation"]);
                            mod_data.Item_Id = Convert.ToString(Dr["Item_Id"]);
                            mod_data.Item_SlNo = Convert.ToString(Dr["Item_SlNo"]);

                            if (List_Type == "Get_Item_in_AMC")
                            {
                                mod_data.Vendor_name = Convert.ToString(Dr["Vendor_name"]);
                                mod_data.AMC_End_Dt = Convert.ToDateTime(Dr["AMC_End_Dt"]);
                            }
                            else if (List_Type== "Get_Item_in_Warranty")
                            {
                                mod_data.Warranty_End_Dt = Convert.ToDateTime(Dr["Warnt_end_DT"]);
                            }
                           
                            mod_data.Asset_Type = Asset_Types;
                            Data.Add(mod_data);
                        }

                    }
                }

            }
            catch (Exception ex) { }

            return Data;

        }


        public int Shift_Warnty_To_Amc(Mod_add_To_Amc Data)
        {
            int status = 1;
            string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_IT_AMC";

                cmd.Connection = con;


                SqlParameter Item_Id = new SqlParameter("@Item_Id", Data.Item_Id);
                cmd.Parameters.Add(Item_Id);

                SqlParameter Vendor_Id = new SqlParameter("@Serial_No", Data.Vendor_Id);
                cmd.Parameters.Add(Vendor_Id);

                SqlParameter AMC_Start_DT = new SqlParameter("@AMC_Start_Dt", Data.AMC_Start_DT);
                cmd.Parameters.Add(AMC_Start_DT);

                SqlParameter AMC_To_Renew = new SqlParameter("@Amc_To_Renew", 1);
                cmd.Parameters.Add(AMC_Start_DT);

                SqlParameter AMC_End_DT = new SqlParameter("@AMC_End_Dt", Data.AMC_End_DT);
                cmd.Parameters.Add(AMC_End_DT);


                con.Open();

                status = cmd.ExecuteNonQuery();



            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }


        public int Remove_From_Amc(string Item_Id)
        {
            int status = 1;
            string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_IT_AMC";

                cmd.Connection = con;


                SqlParameter Sql_Item_Id = new SqlParameter("@Item_Id", Item_Id);
                cmd.Parameters.Add(Sql_Item_Id);

                SqlParameter Sql_Type = new SqlParameter("@Type", "Remove_From_Amc");
                cmd.Parameters.Add(Sql_Type);               

                con.Open();

                status = cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

        public int Add_To_Amc(Mod_Amc_Dtl data)
        {
            int status = 1;
            string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_IT_AMC";

                cmd.Connection = con;

                SqlParameter Sql_Type = new SqlParameter("@Type", "Shift_Warnty_To_Amc");
                cmd.Parameters.Add(Sql_Type);

                SqlParameter AMC_Start_Id = new SqlParameter("@Item_Id", data.AMC_Start_Id);
                cmd.Parameters.Add(AMC_Start_Id);

                SqlParameter AMC_Start_DT = new SqlParameter("@AMC_Start_Dt", data.AMC_Start_DT);
                cmd.Parameters.Add(AMC_Start_DT);

                SqlParameter AMC_End_DT = new SqlParameter("@AMC_End_Dt", data.AMC_End_DT);
                cmd.Parameters.Add(AMC_End_DT);

                SqlParameter AMC_Vendor_Id = new SqlParameter("@AMC_Vendor_Id", data.AMC_Vendor_Id);
                cmd.Parameters.Add(AMC_Vendor_Id);

            
                con.Open();

                status = cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { status = -1; }
            finally { con.Close(); }

            return status;
        }

    }
}