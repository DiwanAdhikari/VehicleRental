﻿using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Utilities
{
    public interface IUtility
    {
        Task<string> GetGenderName(int id);
        Task<FileUploadViewModel> UploadImgReturnPathAndName(string folderName, IFormFile img);
        Task<SelectList> GetGender();
        Task<SelectList> GetVehicleOwner();
        Task<SelectList> GetVehicleCategory();
        Task<SelectList> GetVehicles();
        Task<SelectList> GetVehicleRenters();
        Task<SelectList> GetBrands();
        Task<SelectList> GetCategories();
        Task<string> GetLoginUserIdAsync();
    }
}
 