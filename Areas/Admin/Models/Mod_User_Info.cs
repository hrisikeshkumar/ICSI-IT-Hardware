using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IT_Hardware_Aug2021.Areas.Admin.Models
{
    public class Mod_User_Info
    {
        public string User_ID { get; set; }
        public string User_First_Name { get; set; }
        public string User_Last_Name { get; set; }
        public string User_Email { get; set; }
        public string User_Password { get; set; }
        public string User_Role { get; set; }
        public string Remarks { get; set; }
        public string Create_User { get; set; }
        public DateTime? Create_date { get; set; }
        
    }
}