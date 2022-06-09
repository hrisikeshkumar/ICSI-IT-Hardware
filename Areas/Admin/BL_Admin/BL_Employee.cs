﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using IT_Hardware_Aug2021.Areas.Admin.Models;

namespace IT_Hardware_Aug2021.Areas.Admin.BL_Admin
{
    public class BL_Employee
    {
  
            public List<Mod_Employee> Get_EmployeeData()
            {

                Mod_Employee BL_data;
                List<Mod_Employee> current_data = new List<Mod_Employee>();

                try
                {
                    DataTable dt_Comuter;
                    string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                    SqlConnection con = new SqlConnection(strcon);


                    using (SqlCommand cmd = new SqlCommand("sp_Employee"))
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
                        BL_data = new Mod_Employee();

                        BL_data.Emp_Unique_Id = Convert.ToString(dr["Emp_Unique_Id"]);

                        BL_data.Emp_Name = Convert.ToString(dr["Emp_Name"]);

                        BL_data.Emp_Designation = Convert.ToString(dr["Emp_Designation"]);

                        current_data.Add(BL_data);
                    }

                }
                catch (Exception ex) { }

                return current_data;
            }

            public int Save_Employee_data(Mod_Employee Data, string type, string Unique_Id)
            {
                int status = 1;
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);
                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Computer";

                    cmd.Connection = con;

                    SqlParameter sqlP_type = new SqlParameter("@Type", type);
                    cmd.Parameters.Add(sqlP_type);

                    if (type == "Update" || type == "Delete")
                    {
                        SqlParameter Emp_Unique_Id = new SqlParameter("@Item_Id", Unique_Id);
                        cmd.Parameters.Add(Emp_Unique_Id);
                    }

                    SqlParameter Emp_Code = new SqlParameter("@Item_MakeId", Data.Emp_Code);
                    cmd.Parameters.Add(Emp_Code);

                    SqlParameter Emp_Name = new SqlParameter("@Item_serial_No", Data.Emp_Name);
                    cmd.Parameters.Add(Emp_Name);

                    SqlParameter Emp_Designation = new SqlParameter("@Proc_Date", Data.Emp_Designation);
                    cmd.Parameters.Add(Emp_Designation);

                    SqlParameter Emp_Type = new SqlParameter("@Warnt_end_DT", Data.Emp_Type);
                    cmd.Parameters.Add(Emp_Type);

                    SqlParameter Remarks = new SqlParameter("@Asset_Price", Data.Remarks);
                    cmd.Parameters.Add(Remarks);

                    SqlParameter Asset_Type = new SqlParameter("@Asset_Type", "Employee");
                    cmd.Parameters.Add(Asset_Type);

                    con.Open();

                    cmd.ExecuteNonQuery();

                    status = 0;

                }
                catch (Exception ex) { status = 1; }
                finally { con.Close(); }

                return status;
            }

            public Mod_Employee Get_Data_By_ID(string Unique_Id)
            {
                Mod_Employee Data = new Mod_Employee();

                try
                {
                    DataTable dt_Comuter;
                    string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                    SqlConnection con = new SqlConnection(strcon);


                    using (SqlCommand cmd = new SqlCommand("sp_Employee"))
                    {
                        SqlParameter sqlP_type = new SqlParameter("@Type", "Get_Data_By_ID");
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.Add(sqlP_type);

                        SqlParameter Emp_Unique_Id = new SqlParameter("@Emp_Unique_Id", Unique_Id);
                        cmd.Parameters.Add(Emp_Unique_Id);

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

                    if (dt_Comuter.Rows.Count > 0)
                    {
                        Data.Emp_Unique_Id = Convert.ToString(dt_Comuter.Rows[0]["Emp_Unique_Id"]);
                        Data.Emp_Code = Convert.ToString(dt_Comuter.Rows[0]["Emp_Code"]);
                        Data.Emp_Name = Convert.ToString(dt_Comuter.Rows[0]["Emp_Name"]);
                        Data.Emp_Designation = Convert.ToString(dt_Comuter.Rows[0]["Emp_Designation"]);
                        Data.Emp_Type = Convert.ToString(dt_Comuter.Rows[0]["Emp_Type"]);
                        Data.Remarks = Convert.ToString(dt_Comuter.Rows[0]["Remarks"]);

                    }

                }
                catch (Exception ex) { }

                return Data;
            }


        }
    
}