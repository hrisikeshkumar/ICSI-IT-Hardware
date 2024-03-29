﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_Laptop
    {
        public string Item_id { get; set; }
        [Required]
        public string Item_Make_id { get; set; }
        public List<SelectListItem> Item_Make_List { get; set; }
        public string Item_Model_id { get; set; }
        public List<SelectListItem> Item_Model_List { get; set; }
        public string Item_Type { get; set; }

        [Required]
        public string Item_serial_No { get; set; }
        public string Remarks { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public DateTime? Proc_date { get; set; }

        [Required]
        public DateTime? Warnt_end_dt { get; set; }
        public string Create_usr_id { get; set; }
        public DateTime? Create_date { get; set; }
        public string Verfd_status { get; set; }
        public string Verfd_usr_id { get; set; }
        public DateTime? Verfd_date { get; set; }

    }
}