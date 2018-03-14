using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Form.Examples.Utils
{
    public static class Tools
    {
        public static int _ADULTAGE = 18;

        public static void UploadFile(HttpPostedFileBase hpf)
        {
            //build the file path to save the file in the folder.
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~"), ConfigurationManager.AppSettings["filesPath"], hpf.FileName);
            //Save the file
            hpf.SaveAs(filePath);
        }
    }


}