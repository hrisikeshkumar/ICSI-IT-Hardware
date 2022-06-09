﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_Vendor
    {

        public string Item_Make_id { get; set; }
        public string Item_id { get; set; }
        public string Item_Make { get; set; }
        public string Item_Model { get; set; }
        public string Item_Type { get; set; }
        public string Item_serial_No { get; set; }
        public string Remarks { get; set; }
        public int price { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}")]
        public DateTime? Proc_date { get; set; }
        public DateTime? Warnt_end_dt { get; set; }
        public string Create_usr_id { get; set; }
        public DateTime? Create_date { get; set; }
        public string Verfd_status { get; set; }
        public string Verfd_usr_id { get; set; }
        public DateTime? Verfd_date { get; set; }
    }
}