using IT_Hardware_Aug2021.Areas.Admin.BL_Admin;
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
            return View("~/Areas/Admin/Views/Stock_Report/Budget_Report_Detail.cshtml");
        }

        [Authorize(Roles = "SU, Admin, Manager, InventoryManager, FmsEngineer, ServerEngineer")]
        public ActionResult Post_Budget_Report(string Budget_Year, string Budget_Head, string Report_Type)
        {
            return View();
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