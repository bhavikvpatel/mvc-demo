using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAppMVC.Models
{
    public class UserViewModel : BaseViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<UserDetailModel> Users { get; set; }
    }
}