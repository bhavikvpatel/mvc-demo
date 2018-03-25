using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAppMVC.Models
{
    public abstract class BaseViewModel
    {
        public string Name { get; set; }
        public FilterFormDetails FilterFormDetails { get; set; }      
        public List<CustomFilter> CustomFilters { get; set; } 
        
    }
}