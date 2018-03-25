using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAppMVC.Models
{
    public class CustomModel
    {
      //  public CityMaster CityMaster { get; set; }

    }

    public class HomeData
    {
        public int DataId { get; set; }
        public string DataDetail { get; set; }
    }

    public class SubmitModel
    {
        public int Id { get; set; }
        public string Filter { get; set; }
    }


    public class UserDetailModel
    {
        public string UserName { get; set; }
        public Guid OperationUnitId { get; set; }
        public Guid BrandId { get; set; }
        public int UserId { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}