using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace MVCFilterDemo
{
    public class FileOperations
    {
        public List<FileInfo> GetFile(string filePath)
        {
            List<FileInfo> listFiles = new List<FileInfo>();
            //Path For download From Network Path.  
            //   string fileSavePath = Server.MapPath("~/Documents");//"~\\Files";
            DirectoryInfo dirInfo = new DirectoryInfo(filePath);
            int i = 0;
            foreach (var item in dirInfo.GetFiles())
            {
                listFiles.Add(new FileInfo()
                {
                    FileId = i + 1,
                    FileName = item.Name,
                    FilePath = dirInfo.FullName + @"\" + item.Name
                });
                i = i + 1;
            }
            return listFiles;



            /*
             Directory.EnumerateFiles(Server.MapPath("~/Content/images/thumbs"));
                       @foreach (var fullPath in Model)
                    {
                        var fileName = Path.GetFileName(fullPath);
                        <li>@fileName</li>
                    }
             */
        }

        public IList<SelectListItem> GetFileList(string path)
        {
            var files = GetFile(path);
            var selectList = new List<SelectListItem>();
            foreach (var file in files)
            {
                selectList.Add(new SelectListItem { Text = file.FileName, Value = file.FilePath });
            }
            return selectList;
        }          
    }
    public class FileInfo
    {
        public int FileId
        {
            get;
            set;
        }
        public string FileName
        {
            get;
            set;
        }
        public string FilePath
        {
            get;
            set;
        }
    }
    //public class FileViewModel
    //{
    //    public List<FileInfo> FileList { get; set; }
    //}


    public class FileViewModel
    {
        public List<FileInfo> List { get; set; }
        public IList<string> SelectedFiles { get; set; }
        public IList<SelectListItem> FileList { get; set; }

        public FileViewModel()
        {
            SelectedFiles = new List<string>();
            FileList = new List<SelectListItem>();
        }
    }
}