using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_Budget_Uses
    {
        public string Budget_Head_Id { get; set; }
        public string Budget_Uses_Id { get; set; }
        public string Budget_Name { get; set; }
        public string Budget_Year { get; set; }
        public string Utilization_Details { get; set; }
        public string Budget_Type { get; set; }
        public int Total_Approved_Budget { get; set; }
        public int Amount_Utilized_Before { get; set; }
        public int Balance_Available { get; set; }
        public int Budget_Amount { get; set; }
        public int Remaning_Balance { get; set; }
        public string Remarks { get; set; }
        public DateTime? Processing_Date { get; set; }
        public string Create_User { get; set; }
        public DateTime? Create_date { get; set; }

        public List<SelectListItem> Budget_List { get; set; }

        public List<SelectListItem> Budget_Year_List { get; set; }

        public List<Bud_Uses_List> Bud_us_list { get; set; }
    }

    public class Bud_Uses_List
    {
        public string Budget_Uses_Id { get; set; }
        public string Budget_Name { get; set; }
        public int Budget_Amount { get; set; }
        public string Budget_Type { get; set; }
    } 

}