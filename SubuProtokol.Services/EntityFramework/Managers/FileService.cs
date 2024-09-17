using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SubuProtokol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SubuProtokol.Services.EntityFramework.Managers
{

    public interface IFileService
    {
    
        string Get(string? fileKey);
        string Upload(IFormFile file);
    }
    public  class FileService :IFileService
    {


        private readonly IConfiguration _config;
        private string Url = "";
        private string UserName = "";
        private string Password = "";
        public FileService(IConfiguration config)
        {
            _config = config;
            var fileSettengs = _config.GetSection("FileService");
            Url = fileSettengs.GetSection("Url").Value;
            UserName = fileSettengs.GetSection("UserName").Value;
            Password = fileSettengs.GetSection("Password").Value;
        }

        public string  Get(string? fileKey)
        {

            //var result = new DataResult();
            string result = null;
            //result.Type = Core.Enums.EnumResponseResultType.Error;

            try
            {
                new SDFSClientCore.AppSettings(Url, UserName, Password);

                using (var ms = new MemoryStream())
                {
                    if (fileKey != null)
                    {
                        var file = SDFSClientCore.FileClient.GetUrl(fileKey);
                        if (!string.IsNullOrEmpty(fileKey))
                        {
                            result = file;
                            //result.Type = Core.Enums.EnumResponseResultType.Success;
                        }
                    }
                    else result = null;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }


            return result;
        }

        public string Upload(IFormFile file)
        {
            if (file == null)
            {
                return null;

            }
            //var result = new Result<string>();
            string result = null;
            //result.Type = Core.Enums.EnumResponseResultType.Error;
            new SDFSClientCore.AppSettings(Url, UserName, Password);

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
             
                var fileBytes = ms.ToArray();
                
                var key = SDFSClientCore.FileClient.Upload(UserName, fileBytes, file.FileName, true, UserName, Password);
                if (!string.IsNullOrEmpty(key))
                {
                    result = key;
                    //result.Type = Core.Enums.EnumResponseResultType.Success;
                }
            }
            
            return result;
        }
    }
}
