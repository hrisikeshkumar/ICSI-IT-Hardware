using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_AssetMaster
    {
        public string Asset_ID { get; set; }
        [Required]
        public string Asset_make { get; set; }
        [Required]
        public string Asset_Model { get; set; }
        [Required]
        public string Asset_Type { get; set; }
        public int Asset_Status { get; set; }
        public string Create_User { get; set; }
        public DateTime Create_Date { get; set; }
        public string Update_User { get; set; }
        public DateTime Update_Date { get; set; }

    }
}