﻿using System.Web;
using System.Web.Mvc;

namespace Inisra_Web_App_MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
