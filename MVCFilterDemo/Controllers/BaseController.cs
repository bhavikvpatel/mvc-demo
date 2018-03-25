using DemoAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoAppMVC.Controllers
{
    public class BaseController : Controller
    {
        public int ModuleId { get; set; }
        protected int IDValue { get; set; } = 122;
        public List<CustomFilter> CustomFilters { get; set;}
    }
}