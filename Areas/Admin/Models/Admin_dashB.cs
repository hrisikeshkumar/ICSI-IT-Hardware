using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Asset.Areas.Admin.Models
{
    public class Admin_dashB
    {
        public string Item_Type { get; set; }
        public string Item_Sl_No { get; set; }
        public string Issued_To { get; set; }
        public string Move_From { get; set; }
        public string Move_To { get; set; }
        public DateTime DateofMovement { get; set; }

    }
}