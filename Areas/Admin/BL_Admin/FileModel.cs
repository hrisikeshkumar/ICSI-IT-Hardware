using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Hardware_Aug2021.Areas.Admin.BL_Admin
{
    public class FileModel
    {
        public string File_Id { get; set; }
        public string File_Name { get; set; }
        public string File_Table { get; set; }
        public string User_Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}