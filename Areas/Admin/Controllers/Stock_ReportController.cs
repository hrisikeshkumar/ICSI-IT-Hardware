﻿using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
using IT_Hardware_Aug2021.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using ClosedXML.Excel;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace IT_Hardware_Aug2021.Areas.Admin.Controllers
{
    public class Stock_ReportController : Controller
    {

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Stock_Report_Detail()
        {
            return View("~/Areas/Admin/Views/Stock_Report/Stock_Report_Detail.cshtml");
        }

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Inventary_Report(string Item_Type , string Report_Type)
        {

            BL_StockReport BL = new BL_StockReport();

            List<Mod_StockReport> Model;

            Model = BL.Get_CompData(Item_Type);

            return View("~/Areas/Admin/Views/Stock_Report/Inventary_Report.cshtml", Model);
        }



        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Budget_Report_Detail()
        {
            Mod_Stock_Report Modal = new Mod_Stock_Report();
            BL_Budget_Year b_year = new BL_Budget_Year();
            Modal.BudYear = b_year.budget_year_dropdown();


            return View("~/Areas/Admin/Views/Stock_Report/Budget_Report_Detail.cshtml", Modal);
        }



        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Post_Budget_Report(Mod_Stock_Report mod_data, string Budget_Head, string Report_Type)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
               
                DataTable dt = this.GetCustomers(mod_data.Bud_year_Id);
                wb.Worksheets.Add(dt, "Budget_Sheet");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }

        }


        private DataTable GetCustomers(string Year_Code)
        {
            DataTable ds = new DataTable();
            try
            {
                
                string strcon = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                SqlConnection con = new SqlConnection(strcon);


                using (SqlCommand cmd = new SqlCommand("sp_StockReport"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter Asset_type = new SqlParameter("@Bud_Year", Year_Code.Trim());
                    cmd.Parameters.Add(Asset_type);

                    SqlParameter sqlP_type = new SqlParameter("@Type", "Get_BudList");
                    cmd.Parameters.Add(sqlP_type);


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ds = dt;
                        }
                    }
                }


            }
            catch (Exception ex) { }

            return ds;
        }



        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public FileResult Export(string GridHtml)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }


    }
}