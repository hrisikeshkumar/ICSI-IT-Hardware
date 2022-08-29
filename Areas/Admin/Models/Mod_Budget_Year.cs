using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_Budget_Year
    {
        public string Bud_Id { get; set; }
        [Required]
        public string Bud_Year { get; set; }
        public Boolean default_Bud { get; set; }

        public string Update_UserId { get; set; }

        public List<Mod_Budget_Year> List_Bud_Year{get; set;}
    }

}