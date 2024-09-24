using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace assignment_30.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            //1- Get Located Folder Path

            //wrong way due to static path
            //string FolderPath = D:\Private\Courses\Full Stack\Back - End\Tasks\assignment 30 - MVC03\assignment 30 - solution\assignment 30.PL\wwwroot\Files\Images\

            //string FolderPath = Directory.GetCurrentDirectory() + "wwwroot\\Files" + FolderName;
            //OR
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            //2- Get File Name and make it unique
            //string fileName = file.Name; //to get extention (jpg,pdf,png,....)
            string fileName = $"{Guid.NewGuid()}{file.FileName}"; //Guid to make unique number mixed with letters

            //3- Get File Path [Folder Path + fileName]

            //string filePath=Path.Combine(folderPath, fileName);
            //OR
            string filePath = folderPath + fileName ;

            //4- save File As streams
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
