using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DemoAppMVC.Controllers.UserController;

namespace DemoAppMVC.Models
{
    public class CustomFilter //<T> where T : class
    {
        public string IconClass { get; set; }
        public int FilterId { get; set; }
        public string FilterTitle { get; set; }
        public string FilterFieldName { get; set; }         
        public bool IsEnable { get; set; }
        public bool IsVisible { get; set; }
        public string ControlType { get; set; }
        public List<object> SelectedOptions { get; set; }
        public string DefaultTextValue { get; set; }   
        public List<OptionObject> Options { get; set; }


        public bool IsShowOptionPanel { get; set; }
        public bool IsShowCount { get; set; }
        public bool IsShowMore { get; set; }
        public string PlaceHolder { get; set; }
        public int FilterSequenceNumber { get; set; }

        public int FilterRangeMinValue { get; set; }
        public int FilterRangeMaxValue { get; set; }
        public int FilterRangeDefaultMinValue { get; set; }
        public int FilterRangeDefaultMaxValue { get; set; }


    }                                                       
    public class OptionObject
    {
        public object ObjectId { get; set; }
        //public T ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public int RecordCounts { get; set; }
    }

    public interface IOptionObject
    {                                 
        T ObjectId<T>(T option);
    }
    public class TempOptions : IOptionObject
    {   
        public T ObjectId<T>(T option)
        {
            return option;
        }
    }
}