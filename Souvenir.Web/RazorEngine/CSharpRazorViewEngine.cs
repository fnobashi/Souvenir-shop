using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Souvenir.Web.RazorEngine
{
    public class CSharpRazorViewEngine:RazorViewEngine
    {
        public CSharpRazorViewEngine()
        {
            base.AreaViewLocationFormats = new string[2]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            base.AreaMasterLocationFormats = new string[2]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            base.AreaPartialViewLocationFormats = new string[2]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            base.ViewLocationFormats = new string[2]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            base.MasterLocationFormats = new string[2]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
            };
            base.PartialViewLocationFormats = new string[2]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            base.FileExtensions = new string[1]
            {
                "cshtml" 
            };
        }

    }
}