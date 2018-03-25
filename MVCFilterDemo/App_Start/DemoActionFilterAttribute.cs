using DemoAppMVC.Controllers;
using DemoAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFilterDemo.App_Start
{
    public class DemoActionFilterAttribute : ActionFilterAttribute
    {
        public string CustomFilter { get; set; }
        public int Id { get; set; }
        public int [] CustomFilters { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            int moduleId = ((BaseController)filterContext.Controller).ModuleId;//filterContext.Controller.ValueProvider.GetValue("ModuleId").AttemptedValue;
            //int IDValue = ((BaseController)filterContext.Controller).IDValue;
            List<CustomFilter> customFilters = ((BaseController)filterContext.Controller).CustomFilters;
            var customFilter = CustomFilter;


            //var modelState = context.ModelState;
            //if (!modelState.IsValid)
            //{
            //    var errors = new JObject();
            //    foreach (var key in modelState.Keys)
            //    {
            //        var state = modelState[key];
            //        if (state.Errors.Any())
            //        {
            //            errors[key] = state.Errors.First().ErrorMessage;
            //        }
            //    }
            //    context.Response = context.Request.CreateResponse<JObject>(HttpStatusCode.BadRequest, errors);
            //}
        }
    }

    //public class FilterConfig
    //{
    //    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    //    {
    //        filters.Add(new AuthorizeAttribute());
    //    }
    //}

    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public string BasicRealm { get; set; }
        protected string Username { get; set; }
        protected string Password { get; set; }

        public BasicAuthenticationAttribute(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!String.IsNullOrEmpty(auth))
            {
                var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                if (user.Name == Username && user.Pass == Password) return;
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", String.Format("Basic realm=\"{0}\"", BasicRealm ?? "Ryadel"));
            /// thanks to eismanpat for this line: http://www.ryadel.com/en/http-basic-authentication-asp-net-mvc-using-custom-actionfilter/#comment-2507605761
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}