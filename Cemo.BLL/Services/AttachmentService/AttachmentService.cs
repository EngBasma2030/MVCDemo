using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Cemo.BLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> allowedExtensions =  [ ".png", ".jpg", ".Jepg" ];
        const int maxSize = 2_097_152;
        public string? Uplooad(IFormFile file, string folderName)
        {
            //1. Check Extension
            var extenstion = Path.GetExtension(file.FileName); // Image01.png
            if (!allowedExtensions.Contains(extenstion)) return null;

            //2. Check Size
            if (file.Length == 0 || file.Length > maxSize) return null;

            //3. Get Located Folder Path
            //var folderPath = "D:\\Basma\\Back end\\Asp.net\\MVC 07\\Session 03\\Demo\\MVCDemo\\Demo.PL\\wwwroot\\Files\\images\\"; //local inValid
            //var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{folderName} ";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            //4. Make Attachment Name Unique-- GUID
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";  // Unique

            //5. Get File Path
            var filePath = Path.Combine(folderPath, fileName);

            //6. Create File Stream To Copy File[Unmanaged]
            FileStream fs = new FileStream(filePath, FileMode.Create);

            //7. Use Stream To Copy File
            file.CopyTo(fs);

            //8. Return FileName To Store In Database
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }

       
    }
}
