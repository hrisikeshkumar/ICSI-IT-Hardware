using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_Employee
    {
        public string Emp_Unique_Id { get; set; }
        public string Emp_Code { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Designation { get; set; }
        public string Emp_Type { get; set; }
        public string Remarks { get; set; }
        public string Create_usr_id { get; set; }
        public DateTime? Create_date { get; set; }
        public string Verfd_status { get; set; }
        public string Verfd_usr_id { get; set; }
        public DateTime? Verfd_date { get; set; }

    }
}