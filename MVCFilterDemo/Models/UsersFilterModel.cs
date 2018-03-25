using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFilterDemo
{
    public class UsersFilterModel
    {
        public List<Guid> OperationUnits { get; set; }
        public List<Guid> Brands { get; set; }

        public string SearchText { get; set; }

        public Guid SortCriteria { get; set; }
        public Guid SortOrder { get; set; }  
        public string PriceRate { get; set; }
        public string AverageRating { get; set; }
        public string AgeRange { get; set; }
        public string MinAge { get; set; }
        public string MaxAge { get; set; }
        public List<string> Genders { get; set; }

        public int OtherType { get; set; }
    }
}