using DemoAppMVC.Models;
using MVCFilterDemo.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DemoAppMVC.Controllers
{
    public class DemoController : BaseController
    {
        private static int[] customfilters { get; set; }
        static private int demoId { get; set; }
        public DemoController()
        {
            ModuleId = 10;
            customfilters = CustomFilters.Select(x=>x.FilterId).ToArray();
        }

        // RxTempContext dbContext = new RxTempContext();
        // GET: Demo
        [DemoActionFilterAttribute(CustomFilter = "authInfo",Id = 10)]
        public ActionResult Index()
        {
            return View();
        }

        private List<Option> GetData()
        {
            var filterDetails = new List<FilterDetail>();
            filterDetails.Add(new FilterDetail() { FilterId = 1, FilterName = "Brands", FilterOptions = new FilterOptions() });
            List<Option> options = new List<Option>();
            for (int i = 0; i < 100; i++)
            {
                options.AddRange(filterDetails.First().FilterOptions.Options);
            }
            return options;
        }
        public ContentResult ContentMethod()
        {
            try
            {
                var jss = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                var jsonString = jss.Serialize(GetData());
                return Content(jsonString, "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult JsonMethod()
        {
            try
            {
                var jsonResult = new JsonResult() { MaxJsonLength = Int32.MaxValue };
                //jsonResult.Data = GetData();
                return Json(GetData(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult Demo()
        {

            return View(/*dbContext.CityMasters.ToList()*/);
        }
    }
}