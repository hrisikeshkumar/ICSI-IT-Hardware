using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_Budget
    {
        public string Budget_Head_Id { get; set; }
        public string Budget_Year { get; set; }
        public string Budget_Name { get; set; }
        public string Total_Budget_Amount { get; set; }
        public string Remarks { get; set; }
        public string Create_User { get; set; }
        public DateTime? Create_date { get; set; }

        public List<SelectListItem> Bud_year_List { get; set; }
    }
}