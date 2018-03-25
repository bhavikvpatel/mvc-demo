using DemoAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace DemoAppMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public static List<HomeData> HomeData { get; set; }
        public ActionResult Index()
        {
           // SendMail();
            HomeData = new List<HomeData>();
            HomeData.Add(new Models.HomeData() { DataId = 1, DataDetail = "Demo1" });
            HomeData.Add(new Models.HomeData() { DataId = 2, DataDetail = "Demo2" });
            HomeData.Add(new Models.HomeData() { DataId = 3, DataDetail = "Demo3" });
            HomeData.Add(new Models.HomeData() { DataId = 4, DataDetail = "Demo4" });
            HomeData.Add(new Models.HomeData() { DataId = 5, DataDetail = "Demo5" });
            ViewBag.GeneralData = "From Intial Call Index Data";
            return View(HomeData);
        }

        private void SendMail()
        {
            try
            {
                SmtpSection smtpSec = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;

                var fromAddress = smtpSec.Network.UserName;
                var toAddress = "bhavik.patel@radixweb.com";
                string fromPassword = smtpSec.Network.Password;
                string subject = "Test";
                string body = "Mail";
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = smtpSec.Network.Host;
                    smtp.Port = smtpSec.Network.Port;
                    smtp.EnableSsl = smtpSec.Network.EnableSsl;
                    //  smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    //smtp.Timeout = 20000;
                }

                MailMessage mailMessage = new MailMessage("bhavik.patel@radixweb.com", toAddress);
                mailMessage.Body = body;
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                smtp.Send(mailMessage);
            }
            catch (Exception e)
            {
               // return 1;
            }
        }

        [HttpPost]
        public ActionResult Post(SubmitModel submitModel)
        {
            HomeData = new List<HomeData>();
            HomeData.Add(new Models.HomeData() { DataId = 6, DataDetail = "Demo6" });
            HomeData.Add(new Models.HomeData() { DataId = 7, DataDetail = "Demo7" });
            HomeData.Add(new Models.HomeData() { DataId = 8, DataDetail = "Demo8" });
            HomeData.Add(new Models.HomeData() { DataId = 9, DataDetail = "Demo9" });
            HomeData.Add(new Models.HomeData() { DataId = 10, DataDetail = "Demo10" });

            ViewBag.GeneralData = "After Post Call Index Data";
            ViewBag.SubmitId = submitModel.Id;
            ViewBag.SubmitData = submitModel.Filter;
            return View("Index", HomeData);
        }
        [HttpPost]
        public ActionResult Index(SubmitModel submitModel)
        {
            HomeData = new List<HomeData>();
            HomeData.Add(new Models.HomeData() { DataId = 11, DataDetail = "Demo11" });
            HomeData.Add(new Models.HomeData() { DataId = 12, DataDetail = "Demo12" });
            HomeData.Add(new Models.HomeData() { DataId = 13, DataDetail = "Demo13" });

            ViewBag.SubmitId = submitModel.Id;
            ViewBag.SubmitData = submitModel.Filter;
            ViewBag.GeneralData = "After Another Post Call Index Data";
            return View("Index", HomeData);
        }
        [HttpPost]
        public ActionResult GetData()
        {                 
            return Json(GetUsers());
        }

        private List<HomeData> GetJsonData()
        {
            var homeData = new List<HomeData>();
            homeData.Add(new Models.HomeData() { DataId = 1, DataDetail = "Demo1" });
            homeData.Add(new Models.HomeData() { DataId = 2, DataDetail = "Demo2" });
            homeData.Add(new Models.HomeData() { DataId = 3, DataDetail = "Demo3" });
            homeData.Add(new Models.HomeData() { DataId = 4, DataDetail = "Demo4" });
            homeData.Add(new Models.HomeData() { DataId = 5, DataDetail = "Demo5" });

            return homeData;
        }
        private List<UserDetailModel> GetUsers()
        {
            var users = new List<UserDetailModel>();
            users.Add(new UserDetailModel { UserId = 1, UserName = "Arnetta Wayland", BrandId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb1"),
                OperationUnitId = Guid.Parse("afd76173-86cd-4b17-a1a1-8c73f386dfb7"),
                Age = 20,
                Gender = "Male" });
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
    }
}