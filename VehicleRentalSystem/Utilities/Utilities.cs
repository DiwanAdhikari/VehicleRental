﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VehicleRentalSystem.Data;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Utilities
{
    public class Utilities : IUtility
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Utilities(ApplicationDbContext context, 
            IWebHostEnvironment webHost, 
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _webHostEnvironment = webHost;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<string> GetGenderName(int id)
        {
            return  await _context.Gender.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefaultAsync();
        }
        public async Task<SelectList> GetGender()
        {
            return new SelectList(await _context.Gender.ToListAsync(), "Id", "Name"); ;
        }
        //public async Task<SelectList> GetVehicleOwner()
        //{
        //    return new SelectList(await _context.VehicleOwner.ToListAsync(), "Id", "FullName"); ;
        //}  
        public async Task<SelectList> GetVehicleCategory()
        {
            return new SelectList(await _context.Category.ToListAsync(), "Id", "Name"); ;
        } 
        public async Task<SelectList> GetVehicles()
        {
            return new SelectList(await _context.Vehicle.ToListAsync(), "Id", "Title"); ;
        }
        public async Task<SelectList> GetVehicleRenters()
        {
            return new SelectList(await _context.Renter.ToListAsync(), "Id", "Name"); ;
        } 
        public async Task<SelectList> GetBrands()
        {
            return new SelectList(await _context.Brand.ToListAsync(), "Id", "Name"); ;
        }
        public async Task<SelectList> GetCategories()
        {
            return new SelectList(await _context.Category.ToListAsync(), "Id", "Name"); ;
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

        public Task<SelectList> GetVehicleOwner()
        {
            throw new NotImplementedException();
        }
        public async Task<string> GetLoginUserIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Users.Select(x => x.Id).FirstOrDefaultAsync();
        }

    }
}
