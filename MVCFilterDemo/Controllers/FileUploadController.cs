using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFilterDemo.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Documents"), fileName);
                    file.SaveAs(path);
                }
                ViewBag.Message = "Successfully Uploaded.";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Failed";
                
                return View("Index");
            }             
        }
        public FileResult DownloadFile()
        {
            string fileName = "~\\Documents\\mail.msg";
            try
            {
                return new FilePathResult(fileName, "application/vnd.ms-outlook");

            }
            catch (Exception ex)
            {

                throw ex;
            }
           

        }

    }
}