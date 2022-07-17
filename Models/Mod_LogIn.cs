using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IT_Hardware_Aug2021.Models
{
    public class Mod_LogIn
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
       
        public string Password { get; set; }
    }
}