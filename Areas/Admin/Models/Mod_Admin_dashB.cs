using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Asset.Areas.Admin.Models
{
    public class Mod_Admin_dashB
    {

        public string Proposal_Id { get; set; }
        public string Budget_Name { get; set; }
        public string Budget_Year { get; set; }
        public string Utilization_Details { get; set; }
        public DateTime IT_Initiate_Date { get; set; }
        public string Dte_IT_Copy { get; set; }
        public string Dte_IT_Remarks { get; set; }
        public string FA_Remarks { get; set; }
        public string Secretary_Approval_Required { get; set; }
        public string Sec_Office_Remarks { get; set; }
        public string Purchase_Remarks { get; set; }
        public string Other_Dept_Remarks { get; set; }
        public string Completed_Status { get; set; }

        public List<mod_Admin_Propsal_List> List_Proposal { get; set; }
        public List<mod_Admin_Bill_Process_List> List_Bill_Process { get; set; }
        public List<mod_SLA_Expiary_List> List_SLA_Expiary { get; set; }
    }

    public class mod_Admin_Propsal_List
    {
       public string Proposal_Id { get; set; }
        public string Utilization_Details { get; set; }
        public DateTime IT_Initiate_Date { get; set; }
        public string Status { get; set; }
              
    }

    public class mod_Admin_Bill_Process_List
    {
        public string Proposal_Id { get; set; }
        public string Utilization_Details { get; set; }
        public DateTime IT_Initiate_Date { get; set; }
        public string Status { get; set; }

    }

    public class mod_SLA_Expiary_List
    {
        public string SLA_Id { get; set; }
        public string Vendor_Name { get; set; }
        public DateTime Expiary_Date { get; set; }
        public string Status { get; set; }

    }
}