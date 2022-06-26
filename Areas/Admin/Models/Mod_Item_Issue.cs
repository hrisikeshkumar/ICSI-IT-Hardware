using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_Item_Issue
    {

        public string Item_Issue_Id { get; set; }
        public string Item_Id { get; set; }
        public string Item_Type { get; set; }
        public string Item_SerialNo { get; set; }
        public string Item_Name { get; set; }
        public string Previous_Custady_Id { get; set; }
        public string Previous_Emp_Name { get; set; }
        public string Previous_Emp_Designation { get; set; }
        public string Previous_Emp_Type { get; set; }
        public string Previous_Emp_Dept { get; set; }
        public string Previous_Emp_Location { get; set; }


        public string Transfered_Custady_Id { get; set; }
        public string Transfered_Emp_Name { get; set; }
        public string Transfered_Emp_Designation { get; set; }
        public string Transfered_Emp_Type { get; set; }
        public string Transfered_Emp_Dept { get; set; }
        public string Transfered_Emp_Location { get; set; }
        public DateTime? Issued_date { get; set; }
        public string Remarks { get; set; }
        public string Create_usr_id { get; set; }
        public DateTime? Create_date { get; set; }
        public string Verfd_status { get; set; }
        public string Verfd_usr_id { get; set; }
        public DateTime? Verfd_date { get; set; }
    }
}