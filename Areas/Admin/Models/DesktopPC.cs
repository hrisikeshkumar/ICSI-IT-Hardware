using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class DesktopPC
    {
        public int Item_id { get; set; }
        public string Item_Type  { get; set; }
        public string Item_Model { get; set; }
        public string Item_serial_No { get; set; }
        public string Wrnty_status { get; set; }
        public string Configuration { get; set; }
        public string Operating_System { get; set; }
        public string All_software { get; set; }
        public string Remarks { get; set; }
        public DateTime ? Proc_date { get; set; }
        public DateTime ? Warnt_strt_dt { get; set; }
        public DateTime ? Warnt_end_dt { get; set; }
        public string Emp_code { get; set; }
        public string Emp_Name { get; set; }
        public string Dept { get; set; }
        public string Item_loc { get; set; }
        public string Vndr_Name { get; set; }
        public string Amc_status { get; set; }
        public DateTime ? Amc_strt_dt { get; set; }
        public DateTime ? Amc_end_dt { get; set; }
        public string Verfd_status { get; set; }
        public string Submit_usr_id { get; set; }
        public DateTime ? Submit_date { get; set; }
        public string Verfd_usr_id { get; set; }
        public DateTime ? Verfd_date { get; set; }                          

    }
}