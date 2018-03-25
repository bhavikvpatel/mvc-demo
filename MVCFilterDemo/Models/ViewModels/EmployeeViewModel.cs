using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAppMVC.Models
{
    public class EmployeeViewModel  : BaseViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
       
    }
}