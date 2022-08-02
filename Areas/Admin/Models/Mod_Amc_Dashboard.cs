using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_Amc_Dashboard
    {

        public int PC_AMC { get; set; }
        public int PC_Waranty { get; set; }
        public int Laptop_AMC { get; set; }
        public int Laptop_Waranty { get; set; }
        public int Printer_AMC { get; set; }
        public int Printer_Waranty { get; set; }
        public int Scanner_AMC { get; set; }
        public int Scanner_Waranty { get; set; }
        public int Ups_AMC { get; set; }
        public int Ups_Waranty { get; set; }

    }

    public class Mod_Warranty_Amc
    {
       
        public string Item_Id { get; set; }
        public string Emp_Name { get; set; }
        public string Designation { get; set; }
        public string Item_SlNo { get; set; }
        public string Asset_Type { get; set; }
        public DateTime Warnt_end_DT { get; set; }

    }

}