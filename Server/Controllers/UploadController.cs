using Microsoft.AspNetCore.Mvc;

namespace BlazorEmployeeManagementApp2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public UploadController(IWebHostEnvironment env) 
        {
            _env = env;
        }

        [HttpPost]
        [Route("UploadFiles")]
        public List<string> UploadFiles(List<IFormFile> files)
        {
            List<string> fileNames = new List<string>();
            foreach (var file in files)
            {
                string fileName = file.FileName;
                var path = Path.Combine(_env.ContentRootPath, "images", fileName);

                using FileStream fs = new(path, FileMode.Create);
                file.CopyTo(fs);

                fileNames.Add(fileName);
            }

            return fileNames;
        }
    }
}
