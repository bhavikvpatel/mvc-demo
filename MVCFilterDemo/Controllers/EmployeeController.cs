using DemoAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoAppMVC.Controllers
{
    public class EmployeeController : BaseController
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeViewModel employeeViewModel = SetViewModel();
            return View(employeeViewModel);
        }

        private EmployeeViewModel SetViewModel()
        {
            return new EmployeeViewModel()
            {
                Name = "Employee Page",
                EmployeeId = 9,
                EmployeeName = "Demo Employee",
                FilterFormDetails = new FilterFormDetails()
                {
                    CanSubmit = true,
                    FormAction = "/Employee/Index",
                    FormId = "EmployeeSubmitForm",
                    FormMethod = "Post",
                    FormName = "EmployeeSubmitForm"
                },
                CustomFilters = new List<CustomFilter>()
                {
                    new CustomFilter()
                    {
                        ControlType=ApplicationConstants.Checkbox,
                        FilterFieldName="OperationUnitId",
                         FilterId=1,
                         FilterTitle="Operation Units",
                         IsEnable=true,
                         IsVisible=true,
                         Options = GetObjectData(),
                         SelectedOptions = new List<object>() {Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"),
                             Guid.Parse("da532295-883e-467d-8fbf-b8d63e1095df") }
                    },
                }
            };
        }

        #region Statics
        private List<OptionObject> GetObjectData()
        {
            var optionObject = new List<OptionObject>();
            for (int i = 0; i < 20; i++)
            {


                optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), ObjectName = "Admin ["+i+"]" });
                optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115e5"), ObjectName = "PHP [" + i + "]" });
                optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c8b3"), ObjectName = "Sales [" + i + "]" });
                optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.NewGuid(), ObjectName = "Photo Restoration [" + i + "]" });
                optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1095df"), ObjectName = "Java [" + i + "]" });
                optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.NewGuid(), ObjectName = "System [" + i + "]" });
            }
            return optionObject; //.Cast<object>().ToList();
        }
        #endregion
    }
}