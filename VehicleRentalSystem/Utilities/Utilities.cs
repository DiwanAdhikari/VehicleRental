using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleRentalSystem.Data;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Utilities
{
    public class Utilities : IUtility
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Utilities(ApplicationDbContext context, IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHost;


        }
        public async Task<string> GetGenderName(int id)
        {
            return  await _context.Gender.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefaultAsync();
        }
        public async Task<FileUploadViewModel> UploadImgReturnPathAndName(string folderName, IFormFile file)
        {
            try
            {
                FileUploadViewModel model = new FileUploadViewModel();
                string returnPath = null;
                if (file != null)
                {
                    var fileExt = Path.GetExtension(file.FileName).Substring(1);
                    folderName = string.IsNullOrEmpty(folderName) ? "images" : folderName;
                    folderName = (folderName == "images") ? "images/AppImage/" : "images/" + folderName + "/";
                    model.FileName = Guid.NewGuid().ToString() + "." + fileExt;
                    returnPath = folderName + model.FileName;

                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);// if Path not present than create

                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, returnPath);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    model.FilePath = "/" + returnPath;
                    return model;
                }
                else
                    return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
