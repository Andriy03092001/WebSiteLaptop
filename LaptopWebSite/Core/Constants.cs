using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LaptopWebSite.Core
{
    public class Constants
    {
        public static string ProductDescriptionPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ProductDescriptionPath"];
            }
        }
    }
}