using DemoAppMVC;
using DemoAppMVC.Controllers;
using DemoAppMVC.Models;
using HiQPdf;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Microsoft.OpenPublishing.Build.HtmlConverters.HtmlToPdf;

namespace MVCFilterDemo.Controllers
{
    public class CustomController : BaseController
    {
        // GET: CustomTheme
        public ActionResult Index()
        {
            UserViewModel userViewModel = SetViewModel(new UsersFilterModel());
            return View(userViewModel);
        }


        public ActionResult Pills()
        {
            UserViewModel userViewModel = SetViewModel(new UsersFilterModel());
            return View(userViewModel);
        }
        private UserViewModel SetViewModel(UsersFilterModel usersFilterModel)
        {
            int minimumPriceRate = 0, maximumPriceRate = 0;
            if (usersFilterModel.PriceRate != null)
            {
                List<string> rangeValues = usersFilterModel.PriceRate.Split('-').ToList();
                if (rangeValues != null && rangeValues.Count > 0)
                {
                    minimumPriceRate = Convert.ToInt32(rangeValues[0]);
                    maximumPriceRate = Convert.ToInt32(rangeValues[1]);
                }
            }

            int minimumAvgRate = 0, maximumAvgRate = 0;
            if (usersFilterModel.AverageRating != null)
            {
                List<string> rangeValues = usersFilterModel.AverageRating.Split('-').ToList();
                if (rangeValues != null && rangeValues.Count > 0)
                {
                    minimumAvgRate = Convert.ToInt32(rangeValues[0]);
                    maximumAvgRate = Convert.ToInt32(rangeValues[1]);
                }
            }
            int minimumAge = 1, maximumAge = 100;
            int setMaxAgeValue;
            if (!string.IsNullOrEmpty(usersFilterModel.MaxAge) && int.TryParse(usersFilterModel.MaxAge, out setMaxAgeValue))
            {
                maximumAge = setMaxAgeValue;
            }
            int setMinAgeValue;
            if (!string.IsNullOrEmpty(usersFilterModel.MinAge) && int.TryParse(usersFilterModel.MinAge, out setMinAgeValue))
            {
                minimumAge = setMinAgeValue;
            }
            //if (usersFilterModel.AgeRange != null)
            //{
            //    List<string> rangeValues = usersFilterModel.AgeRange.Split('-').ToList();
            //    if (rangeValues != null && rangeValues.Count > 0)
            //    {
            //        minimumAge = Convert.ToInt32(rangeValues[0]);
            //        maximumAge = Convert.ToInt32(rangeValues[1]);
            //    }
            //}


            var userViewModel = new UserViewModel()
            {
                Name = "Custom Page",
                UserId = 9,
                UserName = "Demo User Name",
                FilterFormDetails = new FilterFormDetails()
                {
                    CanSubmit = true,
                    FormAction = "/Custom/Index",
                    FormId = "UserSubmitForm",
                    FormMethod = "Post",
                    FormName = "UserSubmitForm"
                },
            };
            var userList = GetUsers();
            var filteredUsers = new List<UserDetailModel>();
            filteredUsers.AddRange(userList);
            filteredUsers = SetSelectedValues(usersFilterModel, minimumAge, maximumAge, filteredUsers);
            userViewModel.Users = filteredUsers;//GetUsers();


            var customFilters = new List<CustomFilter>()
                {
                    new CustomFilter()
                    {
                        IconClass = "-icon-search",
                      ControlType=ApplicationConstants.TextBox,
                      FilterFieldName="SearchText",
                      FilterId=10,
                      IsEnable =true,
                      IsVisible=true,
                      FilterTitle="Search",
                      FilterSequenceNumber=1,
                      DefaultTextValue=usersFilterModel.SearchText,
                      PlaceHolder = "search users",

                    },
                    new CustomFilter()
                    {
                        IconClass = "-icon-badge",
                        ControlType=ApplicationConstants.Checkbox,
                        IsShowOptionPanel= true,
                        FilterFieldName="OperationUnits",
                         FilterId=100,
                         FilterTitle="Operation Units",
                         IsEnable=true,
                         IsVisible=true,
                         IsShowMore = true,
                         IsShowCount = true,
                         FilterSequenceNumber=2,
                         Options = GetOperationUnits(userList),
                         SelectedOptions = (usersFilterModel.OperationUnits != null? usersFilterModel.OperationUnits.Cast<object>().ToList() :new List<object>())
                    },
                     new CustomFilter()
                    {
                         IconClass = "-icon-branch",
                        ControlType=ApplicationConstants.Checkbox,
                        IsShowOptionPanel= true,
                        FilterFieldName="Brands",
                         FilterId=200,
                         FilterTitle="Brands",
                         IsEnable=true,
                         IsVisible=true,
                         IsShowMore = true,
                         IsShowCount = true,
                         FilterSequenceNumber=3,
                         Options = GetBrands(userList),
                         SelectedOptions = (usersFilterModel.Brands != null? usersFilterModel.Brands.Cast<object>().ToList() :new List<object>())
                    },

                         new CustomFilter()
                    {
                             IconClass = "-icon-people",
                        ControlType=ApplicationConstants.Checkbox,
                        IsShowOptionPanel= true,
                        FilterFieldName="Gender",
                         FilterId=202,
                         FilterTitle="Gender",
                         IsEnable=true,
                         IsVisible=true,
                         IsShowMore = false,
                         IsShowCount = true,
                         FilterSequenceNumber=5,
                         Options =new List<OptionObject>() { new OptionObject() {ObjectId = "Male",ObjectName="Male", RecordCounts= userList.Count(x=>x.Gender == "Male") },
                                                             new OptionObject() { ObjectId = "Female", ObjectName = "Female", RecordCounts= userList.Count(x=>x.Gender == "Female") } },
                         SelectedOptions = (usersFilterModel.Genders != null? usersFilterModel.Genders.Cast<object>().ToList() :new List<object>())
                    },
                          new CustomFilter()
                        {
                              IconClass = "-icon-alpha",
                            ControlType = ApplicationConstants.RangeSlider,
                            IsShowOptionPanel= true,
                            FilterFieldName = "AgeRange",
                            FilterTitle="Age",
                            FilterId = 111,
                            IsEnable=true,
                            IsVisible=true,
                            IsShowMore =false,
                            FilterSequenceNumber=6,
                            FilterRangeDefaultMinValue =minimumAge,
                            FilterRangeDefaultMaxValue = maximumAge,
                            FilterRangeMinValue = 1,
                            FilterRangeMaxValue= 100,
                            Options = new List<OptionObject>(),
                            SelectedOptions = new List<object>()
                        } ,
                        // new CustomFilter()
                        //{
                        // ControlType=ApplicationConstants.DropdownList,                         
                        // FilterFieldName="SortCriteria",
                        // FilterId=201,
                        // FilterTitle="Sort By",
                        // IsEnable=true,
                        // IsVisible=true,
                        // IsShowMore = false,
                        // FilterSequenceNumber=7,
                        // Options = GetSortingFields(),
                        // SelectedOptions = (usersFilterModel.SortCriteria != null? new List<object>() {usersFilterModel.SortCriteria } :new List<object>())
                        //},
                        new CustomFilter()
                    {
                            IconClass = "-icon-sort",
                        ControlType=ApplicationConstants.DropdownList,
                        IsShowOptionPanel= true,
                        FilterFieldName="SortOrder",
                         FilterId=202,
                         FilterTitle="Sort Order",
                         IsEnable=true,
                         IsVisible=true,
                         IsShowMore = false,
                         FilterSequenceNumber=8,
                         Options =new List<OptionObject>() { new OptionObject() {ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfa1"), ObjectName="ASC" }, new OptionObject() { ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfa2"), ObjectName = "DESC" } },
                         SelectedOptions = (usersFilterModel.SortOrder != null? new List<object>() {usersFilterModel.SortOrder } :new List<object>())
                    } ,
                          new CustomFilter()
                        {
                               IconClass = "-icon-cog",
                         ControlType=ApplicationConstants.DropdownList,
                         FilterFieldName="OtherType",
                         FilterId=201,
                         FilterTitle="Order By",
                         IsEnable=true,
                         IsVisible=true,
                         IsShowMore = false,
                         FilterSequenceNumber=7,
                         Options = GetOtherTypeOptions(),
                         SelectedOptions = new List<object>() {usersFilterModel.OtherType }
                        },
                        //new CustomFilter()
                        //{
                        //    ControlType = ApplicationConstants.RangeSlider,
                        //    FilterFieldName = "PriceRate",
                        //    FilterTitle="Price Rate",
                        //    FilterId = 203,
                        //    IsEnable=true,
                        //    IsVisible=true,
                        //    IsShowMore =false,
                        //    FilterSequenceNumber=8,
                        //    FilterRangeDefaultMinValue =(minimumPriceRate>0?minimumPriceRate: 70),
                        //    FilterRangeDefaultMaxValue = (maximumPriceRate>0?maximumPriceRate: 100),
                        //    FilterRangeMinValue = 10,
                        //    FilterRangeMaxValue= 300,
                        //    Options = new List<OptionObject>(),
                        //    SelectedOptions = new List<Guid>()
                        //},
                        // new CustomFilter()
                        //{
                        //    ControlType = ApplicationConstants.RangeSlider,
                        //    FilterFieldName = "AverageRating",
                        //    FilterTitle="Average Rating",
                        //    FilterId = 204,
                        //    IsEnable=true,
                        //    IsVisible=true,
                        //    IsShowMore =false,
                        //    FilterSequenceNumber=9,
                        //    FilterRangeDefaultMinValue =(minimumAvgRate>0?minimumAvgRate: 2),
                        //    FilterRangeDefaultMaxValue = (maximumAvgRate>0?maximumAvgRate: 8),
                        //    FilterRangeMinValue = 1,
                        //    FilterRangeMaxValue= 10,
                        //    Options = new List<OptionObject>(),
                        //    SelectedOptions = new List<Guid>()
                        //}
                };

            userViewModel.CustomFilters = customFilters.OrderBy(x => x.FilterSequenceNumber).ToList();
            return userViewModel;
        }
        private static List<UserDetailModel> SetSelectedValues(UsersFilterModel usersFilterModel, int minimumAge, int maximumAge, List<UserDetailModel> filteredUsers)
        {
            if (usersFilterModel != null)
            {
                if (usersFilterModel.Genders != null && usersFilterModel.Genders.Count > 0)
                {
                    //if (usersFilterModel.Gender == 0)
                    //    filteredUsers = filteredUsers.Where(x => x.Gender == "Male").ToList();
                    //else if (usersFilterModel.Gender == 1)
                        filteredUsers = filteredUsers.Where(x => usersFilterModel.Genders.Contains(x.Gender)).ToList();
                }
                if (usersFilterModel.OperationUnits != null && usersFilterModel.OperationUnits.Count > 0)
                {
                    filteredUsers = filteredUsers.Where(x => usersFilterModel.OperationUnits.Contains(x.OperationUnitId)).ToList();
                }
                if (usersFilterModel.Brands != null && usersFilterModel.Brands.Count > 0)
                {
                    filteredUsers = filteredUsers.Where(x => usersFilterModel.Brands.Contains(x.BrandId)).ToList();
                }
                if (minimumAge > 0)
                {
                    filteredUsers = filteredUsers.Where(x => x.Age >= minimumAge).ToList();
                }
                if (maximumAge > 0)
                {
                    filteredUsers = filteredUsers.Where(x => x.Age <= maximumAge).ToList();
                }
                if (!string.IsNullOrEmpty(usersFilterModel.SearchText))
                {
                    filteredUsers = filteredUsers.Where(x => x.UserName.ToLower().StartsWith(usersFilterModel.SearchText.ToLower())
                                                        || x.UserName.ToLower().Contains(usersFilterModel.SearchText.ToLower())
                                                        || x.UserName.ToLower().EndsWith(usersFilterModel.SearchText.ToLower())).ToList();
                }

                if (usersFilterModel.SortOrder == Guid.Empty || usersFilterModel.SortOrder != Guid.Empty && usersFilterModel.SortOrder == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfa1"))
                {
                    if (usersFilterModel.SortCriteria == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb3"))
                    {
                        filteredUsers = filteredUsers.OrderBy(x => x.UserName).ToList();
                    }
                    else if (usersFilterModel.SortCriteria == Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d4"))
                    {
                        filteredUsers = filteredUsers.OrderBy(x => x.Age).ToList();
                    }
                    else if (usersFilterModel.SortCriteria == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb5"))
                    {
                        filteredUsers = filteredUsers.OrderBy(x => x.Gender).ToList();
                    }
                    else if (usersFilterModel.SortCriteria == Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c7b6"))
                    {
                        filteredUsers = filteredUsers.OrderBy(x => x.UserId).ToList();
                    }
                }
                else if (usersFilterModel.SortOrder != Guid.Empty && usersFilterModel.SortOrder == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfa2"))
                {
                    if (usersFilterModel.SortCriteria == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb3"))
                    {
                        filteredUsers = filteredUsers.OrderByDescending(x => x.UserName).ToList();
                    }
                    else if (usersFilterModel.SortCriteria == Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d4"))
                    {
                        filteredUsers = filteredUsers.OrderByDescending(x => x.Age).ToList();
                    }
                    else if (usersFilterModel.SortCriteria == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb5"))
                    {
                        filteredUsers = filteredUsers.OrderByDescending(x => x.Gender).ToList();
                    }
                    else if (usersFilterModel.SortCriteria == Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c7b6"))
                    {
                        filteredUsers = filteredUsers.OrderByDescending(x => x.UserId).ToList();
                    }
                }
            }

            return filteredUsers;
        }

        #region Statics
        private List<OptionObject> GetOtherTypeOptions()
        {
            var optionObject = new List<OptionObject>();

            optionObject.Add(new OptionObject { ObjectType = "OTHER", ObjectId = 1, ObjectName = "Other 1" });
            optionObject.Add(new OptionObject { ObjectType = "OTHER", ObjectId = 2, ObjectName = "Other 2" });
            optionObject.Add(new OptionObject { ObjectType = "OTHER", ObjectId = 3, ObjectName = "Other 3" });
            optionObject.Add(new OptionObject { ObjectType = "OTHER", ObjectId = 4, ObjectName = "Other 4" });
            optionObject.Add(new OptionObject { ObjectType = "OTHER", ObjectId = 5, ObjectName = "Other 5" });
            optionObject.Add(new OptionObject { ObjectType = "OTHER", ObjectId = 6, ObjectName = "Other 6" });
            return optionObject;
        }
        private List<UserDetailModel> GetUsers()
        {
            var users = new List<UserDetailModel>();
            users.Add(new UserDetailModel { UserId = 1, UserName = "Arnetta Wayland", BrandId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb1"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 20, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 2, UserName = "Lyda Jonason", BrandId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d5"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 20, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 3, UserName = "Phyliss Enright", BrandId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb1"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb9"), Age = 22, Gender = "Female" });
            users.Add(new UserDetailModel { UserId = 4, UserName = "Torie Scouten", BrandId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d5"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb9"), Age = 23, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 5, UserName = "Jeffery Goza", BrandId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb1"), OperationUnitId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c8b4"), Age = 24, Gender = "Female" });
            users.Add(new UserDetailModel { UserId = 6, UserName = "Delta Carnes", BrandId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d5"), OperationUnitId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1094df"), Age = 26, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 7, UserName = "Migdalia Kreiner", BrandId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb1"), OperationUnitId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c8b5"), Age = 20, Gender = "Female" });
            users.Add(new UserDetailModel { UserId = 8, UserName = "Stefanie Holmquist", BrandId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1055df"), OperationUnitId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c8b5"), Age = 20, Gender = "Female" });
            users.Add(new UserDetailModel { UserId = 9, UserName = "Guillermina Conigliaro", BrandId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1055df"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 22, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 10, UserName = "Sung Mcree", BrandId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1055df"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 10, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 11, UserName = "Pamelia Buescher", BrandId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1055df"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 8, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 12, UserName = "Tobie Ashurst", BrandId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d5"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 90, Gender = "Female" });
            users.Add(new UserDetailModel { UserId = 13, UserName = "Agripina Morton", BrandId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c7b3"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 20, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 14, UserName = "Bianca Dickerman", BrandId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c7b3"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 50, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 15, UserName = "Lina Roland", BrandId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1055df"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 56, Gender = "Female" });
            users.Add(new UserDetailModel { UserId = 16, UserName = "Chadwick Steelman", BrandId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d5"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 20, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 17, UserName = "Helga Broom", BrandId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1055df"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 67, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 18, UserName = "Dora Lettieri", BrandId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d5"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 42, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 19, UserName = "Kurtis Luna", BrandId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d5"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 87, Gender = "Male" });
            users.Add(new UserDetailModel { UserId = 20, UserName = "Cassi Lucht", BrandId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d5"), OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), Age = 99, Gender = "Female" });
            return users;
        }
        private List<OptionObject> GetOperationUnits(List<UserDetailModel> userDetailModels)
        {
            var optionObject = new List<OptionObject>();
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76174-86cd-4b17-a1a1-8c73f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76174-86cd-4b17-a1a1-8c73f386dfb7")), ObjectName = "Dotnet" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7")), ObjectName = "Admin" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115e5"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115e5")), ObjectName = "PHP" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c8b3"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c8b3")), ObjectName = "Sales" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1095df"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("da532295-883e-467d-8fbf-b8d63e1095df")), ObjectName = "Java" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b16-a1a1-8c73f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b16-a1a1-8c73f386dfb7")), ObjectName = "Dotnet" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb8"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb8")), ObjectName = "Admin" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-84cd-4b17-a1a1-8c73f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-84cd-4b17-a1a1-8c73f386dfb7")), ObjectName = "Custom Sales" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115e6"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115e6")), ObjectName = "PHP" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a2a1-8c73f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-a2a1-8c73f386dfb7")), ObjectName = "Network" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c8b5"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c8b5")), ObjectName = "Sales" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a2-8c73f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-a1a2-8c73f386dfb7")), ObjectName = "Photo Restoration" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1094df"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("da532295-883e-467d-8fbf-b8d63e1094df")), ObjectName = "Java" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c43f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-a1a1-8c43f386dfb7")), ObjectName = "System" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f346dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f346dfb7")), ObjectName = "Mobile" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-82cd-4b17-a1a1-8c73f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-82cd-4b17-a1a1-8c73f386dfb7")), ObjectName = "Dotnet" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb9"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb9")), ObjectName = "Admin" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("bfd76173-86cd-4b17-a1a1-8c73f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("bfd76173-86cd-4b17-a1a1-8c73f386dfb7")), ObjectName = "Custom Sales" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115e4"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115e4")), ObjectName = "PHP" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1b1-8c73f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-a1b1-8c73f386dfb7")), ObjectName = "Network" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c8b4"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c8b4")), ObjectName = "Sales" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-b1a1-8c73f386dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-b1a1-8c73f386dfb7")), ObjectName = "Photo Restoration" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1093df"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("da532295-883e-467d-8fbf-b8d63e1093df")), ObjectName = "Java" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f344dfb7"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f344dfb7")), ObjectName = "System" });
            optionObject.Add(new OptionObject { ObjectType = "OperationUnit", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfa6"), RecordCounts = userDetailModels.Count(x => x.OperationUnitId == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfa6")), ObjectName = "Mobile" });


            return optionObject; //.Cast<object>().ToList();
        }
        private List<OptionObject> GetBrands(List<UserDetailModel> userDetailModels)
        {
            var optionObject = new List<OptionObject>();

            optionObject.Add(new OptionObject { ObjectType = "BRANDS", ObjectId = Guid.Parse("afa76173-86cd-4b17-a1a1-8c73f386dfb1"), RecordCounts = userDetailModels.Count(x => x.BrandId == Guid.Parse("afa76173-86cd-4b17-a1a1-8c73f386dfb1")), ObjectName = "Brand 1" });
            optionObject.Add(new OptionObject { ObjectType = "BRANDS", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb1"), RecordCounts = userDetailModels.Count(x => x.BrandId == Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb1")), ObjectName = "Brand 3" });
            optionObject.Add(new OptionObject { ObjectType = "BRANDS", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8d73f386dfb1"), RecordCounts = userDetailModels.Count(x => x.BrandId == Guid.Parse("afd76173-86cd-4b17-a1a1-8d73f386dfb1")), ObjectName = "Brand 2" });
            optionObject.Add(new OptionObject { ObjectType = "BRANDS", ObjectId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d5"), RecordCounts = userDetailModels.Count(x => x.BrandId == Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d5")), ObjectName = "Brand 4" });
            optionObject.Add(new OptionObject { ObjectType = "BRANDS", ObjectId = Guid.Parse("afd76173-44cd-4b17-a1a1-8c73f386dfb1"), RecordCounts = userDetailModels.Count(x => x.BrandId == Guid.Parse("afd76173-44cd-4b17-a1a1-8c73f386dfb1")), ObjectName = "Brand 7" });
            optionObject.Add(new OptionObject { ObjectType = "BRANDS", ObjectId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c7b3"), RecordCounts = userDetailModels.Count(x => x.BrandId == Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c7b3")), ObjectName = "Brand 5" });
            optionObject.Add(new OptionObject { ObjectType = "BRANDS", ObjectId = Guid.Parse("afd76112-86cd-4b17-a1a1-8c73f386dfb1"), RecordCounts = userDetailModels.Count(x => x.BrandId == Guid.Parse("afd76112-86cd-4b17-a1a1-8c73f386dfb1")), ObjectName = "Brand 8" });
            optionObject.Add(new OptionObject { ObjectType = "BRANDS", ObjectId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1055df"), RecordCounts = userDetailModels.Count(x => x.BrandId == Guid.Parse("da532295-883e-467d-8fbf-b8d63e1055df")), ObjectName = "Brand 6" });
            optionObject.Add(new OptionObject { ObjectType = "BRANDS", ObjectId = Guid.Parse("bcd76173-86cd-4b17-a1a1-8c73f386dfb1"), RecordCounts = userDetailModels.Count(x => x.BrandId == Guid.Parse("bcd76173-86cd-4b17-a1a1-8c73f386dfb1")), ObjectName = "Brand 9" });
            optionObject.Add(new OptionObject { ObjectType = "BRANDS", ObjectId = Guid.Parse("afd76173-48dd-4b17-a1a1-8c73f386dfb1"), RecordCounts = userDetailModels.Count(x => x.BrandId == Guid.Parse("afd76173-48dd-4b17-a1a1-8c73f386dfb1")), ObjectName = "Brand 10" });
            return optionObject; //.Cast<object>().ToList();
        }
        private List<OptionObject> GetSortingFields()
        {
            var optionObject = new List<OptionObject>();

            optionObject.Add(new OptionObject { ObjectType = "SORT", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb1"), ObjectName = "Operation Units" });
            optionObject.Add(new OptionObject { ObjectType = "SORT", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb2"), ObjectName = "Brands" });
            optionObject.Add(new OptionObject { ObjectType = "SORT", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb3"), ObjectName = "User Name" });
            optionObject.Add(new OptionObject { ObjectType = "SORT", ObjectId = Guid.Parse("92120e72-5ab3-4add-a255-c5514e9115d4"), ObjectName = "Age" });
            optionObject.Add(new OptionObject { ObjectType = "SORT", ObjectId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb5"), ObjectName = "Gender" });
            optionObject.Add(new OptionObject { ObjectType = "SORT", ObjectId = Guid.Parse("5e0d0990-f3c6-40ae-b2af-3e7be2b3c7b6"), ObjectName = "User Id" });
            //optionObject.Add(new OptionObject { ObjectType = "SORT", ObjectId = Guid.NewGuid(), ObjectName = "Sort 8" });
            //optionObject.Add(new OptionObject { ObjectType = "SORT", ObjectId = Guid.Parse("da532295-883e-467d-8fbf-b8d63e1055df"), ObjectName = "Sort 6" });
            //optionObject.Add(new OptionObject { ObjectType = "SORT", ObjectId = Guid.NewGuid(), ObjectName = "Sort 9" });
            //optionObject.Add(new OptionObject { ObjectType = "SORT", ObjectId = Guid.NewGuid(), ObjectName = "Sort 10" });
            return optionObject; //.Cast<object>().ToList();
        }
        #endregion


        public ActionResult HtmlPdf()
        {

            UserViewModel userViewModel = SetViewModel(new UsersFilterModel());
            return View(userViewModel);
        }
        public string RenderViewAsString(string viewName, object model)
        {
            // create a string writer to receive the HTML code
            StringWriter stringWriter = new StringWriter();

            // get the view to render
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext,
                      viewName, null);
            // create a context to render a view based on a model
            ViewContext viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                new ViewDataDictionary(model),
                new TempDataDictionary(),
                stringWriter
            );

            // render the view to a HTML code
            viewResult.View.Render(viewContext, stringWriter);

            // return the HTML code
            return stringWriter.ToString();
        }
        //[HttpPost]
        public ActionResult ConvertHtmlPageToPdf()
        {
            // get the HTML code of this view
            string htmlToConvert = RenderViewAsString("Index", null);

            // the base URL to resolve relative images and css
            String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
            String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length -
                "Home/ConvertThisPageToPdf".Length);

            // instantiate the HiQPdf HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            // hide the button in the created PDF
            htmlToPdfConverter.HiddenHtmlElements = new string[]
                       { "#convertThisPageButtonDiv" };

            // render the HTML code as PDF in memory
            byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);

            // send the PDF file to browser
            FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "ThisMvcViewToPdf.pdf";

            return fileResult;
        }
        [HttpPost]
        public ActionResult Index(UsersFilterModel usersFilterModel)
        {
            if (usersFilterModel == null)
                usersFilterModel = new UsersFilterModel();
            //var value = OperationUnits[0];

            UserViewModel userViewModel = SetViewModel(usersFilterModel);
            return View(userViewModel);
        }

                                   
        public ActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Payment(PaymentModel paymentModel)
        {
            Item item = new Item();
            item.name = "Demo Item";
            item.currency = "USD";
            item.price = "5";
            item.quantity = "1";
            item.sku = "sku";                                                    

            List<Item> itms = new List<Item>();
            itms.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = itms;

            //Address for the payment
            Address billingAddress = new Address();
            billingAddress.city = "NewYork";
            billingAddress.country_code = "US";
            billingAddress.line1 = "23rd street kew gardens";
            billingAddress.postal_code = "43210";
            billingAddress.state = "NY";


            //Now Create an object of credit card and add above details to it
            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = "874";
            crdtCard.expire_month = 7;
            crdtCard.expire_year = 2022;
            crdtCard.first_name = "Bhavik";
            crdtCard.last_name = "Patel";
            crdtCard.number = "4032031333187283";
            crdtCard.type = "visa";

            // Specify details of your payment amount.
            Details details = new Details();
            details.shipping = "1";
            details.subtotal = "5";
            details.tax = "1";

            // Specify your total payment amount and assign the details object
            Amount amnt = new Amount();
            amnt.currency = "USD";
            // Total = shipping tax + subtotal.
            amnt.total = "7";
            amnt.details = details;

            // Now make a trasaction object and assign the Amount object
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "Description about the payment amount.";
            tran.item_list = itemList;
            tran.invoice_number = "your invoice number which you are generating";

            // Now, we have to make a list of trasaction and add the trasactions object
            // to this list. You can create one or more object as per your requirements

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // Now we need to specify the FundingInstrument of the Payer
            // for credit card payments, set the CreditCard which we made above

            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            // The Payment creation API requires a list of FundingIntrument    
            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            // Now create Payer object and assign the fundinginstrument list to the object    
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            // finally create the payment object and assign the payer object & transaction list to it
            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            try
            {
                //getting context from the paypal, basically we are sending the clientID and clientSecret key in this function 
                //to the get the context from the paypal API to make the payment for which we have created the object above.

                //Code for the configuration class is provided next

                // Basically, apiContext has a accesstoken which is sent by the paypal to authenticate the payment to facilitator account. An access token could be an alphanumeric string

                APIContext apiContext = PaypalConfiguration.GetAPIContext();

                // Create is a Payment class function which actually sends the payment details to the paypal API for the payment. The function is passed with the ApiContext which we received above.

                Payment createdPayment = pymnt.Create(apiContext);

                //if the createdPayment.State is "approved" it means the payment was successfull else not

                if (createdPayment.state.ToLower() != "approved")
                {
                    ViewBag.Message = "Failed.";
                    return View("Payment");
                }
            }
            catch (PayPal.PayPalException ex)
            {
                ViewBag.Message = "Exception: "+ex.Message;

                return View("Payment");
            }

            ViewBag.Message = "Success";
            return View();
        }
    }
}