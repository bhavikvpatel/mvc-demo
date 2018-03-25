using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAppMVC.Models
{
    public class FilterFormDetails
    {
        public string FormId { get; set; }
        public string FormName { get; set; }
        public string FormAction { get; set; }
        public string FormMethod { get; set; }
        public bool CanSubmit { get; set; }
                
    }
}