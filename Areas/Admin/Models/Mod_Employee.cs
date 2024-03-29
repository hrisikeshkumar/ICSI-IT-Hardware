﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_Employee
    {
        public string Emp_Unique_Id { get; set; }
        public string Emp_Code { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Emp_Name { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Emp_Designation { get; set; }

        public string Emp_Designation_Name { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Emp_Dept { get; set; }
        public string Emp_Dept_Name { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Emp_Type { get; set; }
        public List<SelectListItem> Designation_List { get; set; }
        public List<SelectListItem> Dept_List { get; set; }
        public List<SelectListItem> Emp_Type_List { get; set; }
        public string Remarks { get; set; }
        public string Location { get; set; }
        public string Create_usr_id { get; set; }
        public DateTime? Create_date { get; set; }
        public string Verfd_status { get; set; }
        public string Verfd_usr_id { get; set; }
        public DateTime? Verfd_date { get; set; }

    }


}