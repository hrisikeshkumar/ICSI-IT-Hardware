using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_Stock_Report
    {
        public string Bud_year_Id { get; set; }
        public List<SelectListItem> BudYear { get; set; }
    }
}