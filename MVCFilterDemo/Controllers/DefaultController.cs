using MVCFilterDemo.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFilterDemo.Controllers
{
    [BasicAuthenticationAttribute("bhavik", "patel", BasicRealm = "your-realm")]
    public class DefaultController : Controller
    {
        // GET: Default
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // [Authorize(Roles = "Managers")]
        public ActionResult Index()
        {
            FileViewModel fileModel = SetViewModel();
            return View(fileModel);
        }

        private FileViewModel SetViewModel()
        {
            var fileModel = new FileViewModel();
            var operations = new FileOperations();
            var path = Server.MapPath("~/Files");
            fileModel.List = operations.GetFile(path);
            fileModel.FileList = operations.GetFileList(path);
            return fileModel;
        }

        [HttpPost]
        public ActionResult Index(FileViewModel fileModel)
        {
            if (fileModel.SelectedFiles != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        for (int i = 0; i < fileModel.SelectedFiles.Count; i++)
                        {
                            var fileName = fileModel.SelectedFiles[i].Substring(fileModel.SelectedFiles[i].LastIndexOf(@"\") + 1, (fileModel.SelectedFiles[i].Length - (fileModel.SelectedFiles[i].LastIndexOf(@"\") +1)));
                            ziparchive.CreateEntryFromFile(fileModel.SelectedFiles[i], fileName);
                        }
                    }
                    return File(memoryStream.ToArray(), "application/zip", "SelectedFiles.zip");
                }
            }
            FileViewModel model = SetViewModel();
            ViewBag.Message = "Failed";
            return View(model);
        }
        public ActionResult DownloadFile(string filename, string path)
        {
            return File(path, CommonFunctions.GetMimeType(Path.GetExtension(filename)));
            //return new FilePathResult(path, CommonFunctions.GetMimeType(Path.GetExtension(filename)));
        }

        public ActionResult Download()
        {
            var operations = new FileOperations();
            //////int CurrentFileID = Convert.ToInt32(FileID);  
            var path = Server.MapPath("~/Files");
            var filesCol = operations.GetFile(path).ToList();
            using (var memoryStream = new MemoryStream())
            {
                using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    for (int i = 0; i < filesCol.Count; i++)
                    {
                        ziparchive.CreateEntryFromFile(filesCol[i].FilePath, filesCol[i].FileName);
                    }
                }
                return File(memoryStream.ToArray(), "application/zip", "AllFiles.zip");
            }
        }
    }
}