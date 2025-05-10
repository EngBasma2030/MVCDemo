using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cemo.BLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        // upload attachment
        public string? Uplooad(IFormFile file, string folderName);
        // delete 
        bool Delete(string filePath);
    }
}
