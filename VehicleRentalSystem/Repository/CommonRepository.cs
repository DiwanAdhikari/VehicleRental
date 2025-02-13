using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Transactions;
using VehicleRentalSystem.Data;
using VehicleRentalSystem.Interface;
using VehicleRentalSystem.Models;
using VehicleRentalSystem.Utilities;

namespace VehicleRentalSystem.Repository
{

    public class CommonRepository : ICommon
    {
        private readonly ApplicationDbContext _context;
        private readonly IUtility _utility;
        private readonly string _userId = null;
        //New Aded
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public CommonRepository(ApplicationDbContext context,
            IHttpContextAccessor httpContext,
            ILogger<CommonRepository> logger, 
            UserManager<ApplicationUser> userManager,   
            IUtility utility)
        {
            _context = context;
            _utility = utility;
            _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _userManager = userManager;
        }
        #region Category
        public async Task<List<CategoryViewModel>> GetAllCategory()
        {
            return await _context.Category
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();
        }
        public async Task<CategoryViewModel> GetCategoryById(int? id)
        {
            return await _context.Category.Where(x => x.Id == id)
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).FirstOrDefaultAsync() ?? new CategoryViewModel();
        }
        public async Task<bool> InsertUpdate(CategoryViewModel model)
        {

            try
            {
                if (model.Id > 0)
                {
                    var category = await _context.Category.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (category != null)
                    {
                        category.Name = model.Name;
                        _context.Entry(category).State = EntityState.Modified;
                    }
                }
                else
                {
                    var add = new Category()
                    {
                        Name = model.Name,

                    };
                    await _context.Category.AddAsync(add);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        //#region VehicleOwner
        //public async Task<List<VehicleOwnerViewModel>> GetAllOwner()
        //{
        //    return await _context.VehicleOwner.Where(x=>x.IsActive==true)
        //        .Select(x => new VehicleOwnerViewModel()
        //        {
        //            Id = x.Id,
        //            FullName = x.FullName,
        //            PhoneNumber = x.PhoneNumber,
        //            Email = x.Email,
        //            Address = x.Address,
        //            GenderName = x.Gender.Name,
        //            ProfilePictureUrl = x.ProfilePicture,
        //            IsActive = x.IsActive,
        //        }).ToListAsync();
        //}
        //public async Task<VehicleOwnerViewModel> GetVehicleOwenerById(int? id)
        //{
        //    return await _context.VehicleOwner.Where(x => x.Id == id)
        //        .Select(x => new VehicleOwnerViewModel()
        //        {
        //            Id = x.Id,
        //            FullName = x.FullName,
        //            PhoneNumber = x.PhoneNumber,
        //            Email = x.Email,
        //            Address = x.Address,
        //            GenderId = x.GenderId,
        //            GenderName = x.Gender.Name,
        //            ProfilePictureUrl = x.ProfilePicture,
        //            IsActive = x.IsActive,
        //        }).FirstOrDefaultAsync() ?? new VehicleOwnerViewModel();
        //}
        //public async Task<bool> InsertUpdateOwner(VehicleOwnerViewModel model)
        //{

        //    var img = await _utility.UploadImgReturnPathAndName("Owner", model.ProfilePicture);
        //    try
        //    {
        //        if (model.Id > 0)
        //        {
        //            var owner = await _context.VehicleOwner.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        //            if (owner != null)
        //            {
        //                owner.FullName = model.FullName;
        //                owner.PhoneNumber = model.PhoneNumber;
        //                owner.Email = model.Email;
        //                owner.Address = model.Address;
        //                owner.GenderId = model.GenderId;
        //                owner.ProfilePicture = img.FilePath;
        //                _context.Entry(owner).State = EntityState.Modified;
        //            }
        //        }
        //        else
        //        {
        //            var owneradd = new VehicleOwner()
        //            {
        //                FullName = model.FullName,
        //                PhoneNumber = model.PhoneNumber,
        //                Email = model.Email,
        //                Address = model.Address,
        //                GenderId = model.GenderId,
        //                ProfilePicture = img.FilePath,
        //                IsActive=true,

        //            };
        //            await _context.VehicleOwner.AddAsync(owneradd);
        //            await _context.SaveChangesAsync();
        //        }
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public async Task<bool> DeleteOwner(int id)
        //{
        //    var data = await _context.VehicleOwner.FirstOrDefaultAsync(x => x.Id == id);
        //    if (data != null)
        //    {
        //        data.IsActive = false;
        //        _context.Entry(data).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}
        //#endregion
        #region VehicleRenter
        public async Task<List<RenterViewModel>> GetAllRenter()
        {
            return await _context.Renter.Where(x => x.IsActive == true)
                .Select(x => new RenterViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    CitizenshipNumber = x.CitizenshipNumber,
                    DateOfBirth = x.DateOfBirth,
                    LicenseNumber = x.LicenseNumber,
                    Address = x.Address,
                    GenderName = x.Gender.Name,
                    ProfilePictureUrl = x.ProfilePicture
                }).ToListAsync();
        }
        public async Task<RenterViewModel> GetRenterById(int? id)
        {
            return await _context.Renter.Where(x => x.Id == id)
                .Select(x => new RenterViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    CitizenshipNumber = x.CitizenshipNumber,
                    DateOfBirth = x.DateOfBirth,
                    LicenseNumber = x.LicenseNumber,
                    Address = x.Address,
                    GenderId = x.GenderId,
                    GenderName = x.Gender.Name,
                    ProfilePictureUrl = x.ProfilePicture,
                    IsActive = x.IsActive,
                }).FirstOrDefaultAsync() ?? new RenterViewModel();
        }
        public async Task<bool> InsertUpdateRenter(RenterViewModel model)
        {

            var img = await _utility.UploadImgReturnPathAndName("Gaunpalika", model.ProfilePicture);
            try
            {
                if (model.Id > 0)
                {
                    var owner = await _context.Renter.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (owner != null)
                    {
                        owner.Name = model.Name;
                        owner.PhoneNumber = model.PhoneNumber;
                        owner.Email = model.Email;
                        owner.Address = model.Address;
                        owner.GenderId = model.GenderId;
                        owner.CitizenshipNumber = model.CitizenshipNumber;
                        owner.DateOfBirth = model.DateOfBirth;
                        owner.LicenseNumber = model.LicenseNumber;
                        owner.ProfilePicture = img.FilePath;
                        _context.Entry(owner).State = EntityState.Modified;
                    }
                }
                else
                {
                    var renteradd = new Renter()
                    {
                        GenderId = model.GenderId,
                        Name = model.Name,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Address = model.Address,
                        CitizenshipNumber = model.CitizenshipNumber,
                        LicenseNumber = model.LicenseNumber,
                        DateOfBirth = model.DateOfBirth,
                        ProfilePicture = img.FilePath,
                        IsActive = true,
                    };
                    await _context.Renter.AddAsync(renteradd);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region VehicleRegister
        public async Task<List<VehicleViewModel>> GetAllVehicles()
        {
            return await _context.Vehicle.Where(x => x.IsActive == true)
                .Select(x => new VehicleViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    RentalPrice = x.RentalPrice,
                    Availability = x.Availability,
                    Terms = x.Terms,
                    ImageUrl = x.ImageUrl,
                }).ToListAsync();
        }
        public async Task<VehicleViewModel> GetVehiclesById(int? id)
        {
            return await _context.Vehicle.Where(x => x.Id == id)
                .Select(x => new VehicleViewModel()
                {
                    Id = x.Id,
                    VehicleOwnerId = x.VehicleOwnerId,
                    Title = x.Title,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    RentalPrice = x.RentalPrice,
                    Availability = x.Availability,
                    Terms = x.Terms,
                    ImageUrl = x.ImageUrl,
                    IsActive = x.IsActive,
                }).FirstOrDefaultAsync() ?? new VehicleViewModel();
        }
        public async Task<bool> InsertUpdateVehicle(VehicleViewModel model)
        {

            try
            {
                if (model.Id > 0)
                {
                    var vehical = await _context.Vehicle.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (vehical != null)
                    {
                        vehical.VehicleOwnerId = model.VehicleOwnerId;
                        vehical.Title = model.Title;
                        vehical.Description = model.Description;
                        vehical.CategoryId = model.CategoryId;
                        vehical.RentalPrice = model.RentalPrice;
                        vehical.Availability = model.Availability;
                        vehical.Terms = model.Terms;
                        _context.Entry(vehical).State = EntityState.Modified;
                    }
                }
                else
                {
                    var addvehicle = new Vehicle()
                    {
                        VehicleOwnerId = model.VehicleOwnerId,
                        Title = model.Title,
                        Description = model.Description,
                        CategoryId = model.CategoryId,
                        RentalPrice = model.RentalPrice,
                        Availability = model.Availability,
                        Terms = model.Terms,
                        IsActive = true,
                    };
                    await _context.Vehicle.AddAsync(addvehicle);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region Booking
        public async Task<List<BookingViewModel>> GetAllBooking()
        {
            return await _context.Booking
                .Select(x => new BookingViewModel()
                {
                    Id = x.Id,
                    VehicleId = x.VehicleId,
                    RenterId = x.RenterId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Status = x.Status,
                    CreatedBy = _context.Users.Where(a => a.Id == x.CreatedBy).Select(a => a.FullName).FirstOrDefault(),
                    VehicleName = x.Vehicle.Title,
                    RenterName = x.Renter.Name,
                }).ToListAsync();
        }
        public async Task<BookingViewModel> GetBookingById(int? id)
        {
            return await _context.Booking.Where(x => x.Id == id)
                .Select(x => new BookingViewModel()
                {
                    Id = x.Id,
                    VehicleId = x.VehicleId,
                    RenterId = x.RenterId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Status = x.Status,
                }).FirstOrDefaultAsync() ?? new BookingViewModel();
        }
        public async Task<bool> InsertUpdateBooking(BookingViewModel model)
        {

            try
            {
                if (model.Id > 0)
                {
                    var book = await _context.Booking.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (book != null)
                    {
                        book.VehicleId = model.VehicleId;
                        book.RenterId = model.RenterId;
                        book.StartDate = model.StartDate;
                        book.EndDate = model.EndDate;
                        book.Status = true;
                        _context.Entry(book).State = EntityState.Modified;
                    }
                }
                else
                {
                    var booking = new Booking()
                    {
                        VehicleId = model.VehicleId,
                        RenterId = model.RenterId,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        Status = true,
                        CreatedBy = _userId,

                    };
                    await _context.Booking.AddAsync(booking);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion  
        #region Payment
        public async Task<List<PaymentViewModel>> GetAllPayment()
        {
            return await _context.Payment
                .Select(x => new PaymentViewModel()
                {
                    Id = x.Id,
                    BookingId = x.BookingId,
                    Amount = x.Amount,
                    PaymentDate = x.PaymentDate,
                    PaymentMethod = x.PaymentMethod,
                }).ToListAsync();
        }
        public async Task<PaymentViewModel> GetPaymentById(int? id)
        {
            return await _context.Payment.Where(x => x.Id == id)
                .Select(x => new PaymentViewModel()
                {
                    Id = x.Id,
                    BookingId = x.BookingId,
                    Amount = x.Amount,
                    PaymentDate = x.PaymentDate,
                    PaymentMethod = x.PaymentMethod,
                }).FirstOrDefaultAsync() ?? new PaymentViewModel();
        }
        public async Task<bool> InsertUpdatePayment(PaymentViewModel model)
        {

            try
            {
                if (model.Id > 0)
                {
                    var pay = await _context.Payment.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (pay != null)
                    {
                        pay.BookingId = model.BookingId;
                        pay.Amount = model.Amount;
                        pay.PaymentDate = model.PaymentDate;
                        pay.PaymentMethod = model.PaymentMethod;
                        _context.Entry(pay).State = EntityState.Modified;
                    }
                }
                else
                {
                    var payment = new Payment()
                    {
                        BookingId = model.BookingId,
                        Amount = model.Amount,
                        PaymentDate = model.PaymentDate,
                        PaymentMethod = model.PaymentMethod,
                    };
                    await _context.Payment.AddAsync(payment);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion 
        #region Review
        public async Task<List<ReviewViewModel>> GetAllReview()
        {
            return await _context.Review
                .Select(x => new ReviewViewModel()
                {
                    Id = x.Id,
                    Rating = x.Rating,
                    Comments = x.Comments,
                    RenterName = x.Renter.Name,
                    VehicleName = x.Vehicle.Title,
                }).ToListAsync();
        }
        public async Task<ReviewViewModel> GetReviewById(int? id)
        {
            return await _context.Review.Where(x => x.Id == id)
                .Select(x => new ReviewViewModel()
                {
                    Id = x.Id,
                    Rating = x.Rating,
                    Comments = x.Comments,
                    RenterId = x.Renter.Id,
                    VehicleId = x.Vehicle.Id,
                }).FirstOrDefaultAsync() ?? new ReviewViewModel();
        }
        public async Task<bool> InsertUpdateReview(ReviewViewModel model)
        {

            try
            {
                if (model.Id > 0)
                {
                    var review = await _context.Review.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (review != null)
                    {
                        review.RenterId = model.RenterId;
                        review.Rating = model.Rating;
                        review.Comments = model.Comments;
                        review.VehicleId = model.VehicleId;
                        _context.Entry(review).State = EntityState.Modified;
                    }
                }
                else
                {
                    var reviewAdd = new Review()
                    {
                        RenterId = model.Id,
                        Rating = model.Rating,
                        Comments = model.Comments,
                        VehicleId = model.VehicleId,
                    };
                    await _context.Review.AddAsync(reviewAdd);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<VehicleOwnerViewModel>> GetAllOwner()
        {
            return await _context.VehicleOwnerBasicInfo
              .Select(x => new VehicleOwnerViewModel()
              {
                  VehicleOwnerId = x.Id,
                  FullName = x.FullName,
                  DateOfBirth = x.DateOfBirth,
                  Email = x.Email,
                  PhoneNumber = x.PhoneNumber,
                  Address = x.Address,
                  GenderName = x.Gender.Name,
              }).ToListAsync();

        }

        public async Task<VehicleOwnerViewModel> GetVehicleOwenerById(int? id)
        {
            VehicleOwnerViewModel model = new VehicleOwnerViewModel();
            var data = await (
 from basic in _context.VehicleOwnerBasicInfo
 join details in _context.VehicleDetails on basic.Id equals details.VehicleOwnerBasicInfoId into ownerdetails
 from detail in ownerdetails.DefaultIfEmpty()
 where basic.Id == id
 select new VehicleOwnerViewModel
 {
     FullName = basic.FullName,
     DateOfBirth = basic.DateOfBirth,
     Email = basic.Email,
     PhoneNumber = basic.PhoneNumber,
     Address = basic.Address,
     GenderName = basic.Gender.Name,
     GenderId = basic.GenderId,
     IsActive = basic.IsActive,
     BrandId = detail.BrandId,
     BrandName = detail.Brand.Name,
     BillbookNumber = detail.BillbookNumber,
     ProductionYear = detail.ProductionYear,
     RegistrationNumber = detail.RegistrationNumber,
     VehiclePhotos = detail.VehiclePhotos,
     BillbookImage = detail.BillbookImage,
 }).FirstOrDefaultAsync();
            return data ?? new VehicleOwnerViewModel();

        }

        public async Task<bool> InsertUpdateOwner(VehicleOwnerViewModel model)
        {
        
            try
            {
                if (model.VehicleOwnerId > 0)
                {
                    var data = await _context.VehicleOwnerBasicInfo.FirstOrDefaultAsync(x => x.Id == model.VehicleOwnerId);
                    if (data != null)
                    {
                        data.FullName = model.FullName;
                        data.DateOfBirth = model.DateOfBirth;
                        data.Email = model.Email;
                        data.GenderId = model.GenderId;
                        data.PhoneNumber = model.PhoneNumber;
                        data.Address = model.Address;
                        data.IsActive = model.IsActive;
                        _context.Entry(data).State = EntityState.Modified;
                    }
                    var details = await _context.VehicleDetails.Where(x => x.VehicleOwnerBasicInfoId == model.VehicleOwnerId).FirstOrDefaultAsync();
                    if (details != null)
                    {
                        details.VehicleOwnerBasicInfoId = model.VehicleOwnerId;
                        details.BrandId = model.BrandId;
                        details.BillbookNumber = model.BillbookNumber;
                        details.ProductionYear = model.ProductionYear;
                        details.RegistrationNumber = model.RegistrationNumber;
                        details.BillbookImage = _utility.UploadImgReturnPathAndName("Billbook", model.BillbookImageURL).Result.FilePath;
                        details.VehiclePhotos = _utility.UploadImgReturnPathAndName("VehiclePhotos", model.VehiclePhotosURL).Result.FilePath;

                        _context.Entry(details).State = EntityState.Modified;
                    }

                }
                else
                {
                    var Billbook = await _utility.UploadImgReturnPathAndName("Billbook", model.BillbookImageURL);
                    var VehiclePhotos = await _utility.UploadImgReturnPathAndName("VehiclePhotos", model.VehiclePhotosURL);

                    var VehicleOwnerBasicInfo = new VehicleOwnerBasicInfo()
                    {
                        FullName = model.FullName,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        GenderId = model.GenderId,
                        PhoneNumber = model.PhoneNumber,
                        Address = model.Address,
                        IsActive = true,
                    };
                    await _context.VehicleOwnerBasicInfo.AddAsync(VehicleOwnerBasicInfo);
                    _context.SaveChanges();
                    var VehicleDetails = new VehicleDetails()
                    {
                        VehicleOwnerBasicInfoId = VehicleOwnerBasicInfo.Id,
                        BrandId = model.BrandId,
                        BillbookNumber = model.BillbookNumber,
                        ProductionYear = model.ProductionYear,
                        RegistrationNumber = model.RegistrationNumber,
                        BillbookImage = Billbook.FilePath,
                        VehiclePhotos = Billbook.FilePath,
                    };
                    await _context.VehicleDetails.AddAsync(VehicleDetails);
                    _context.SaveChanges();
                    //var userinfo = _context.Users.Where(x => x.UserName == VehicleOwnerBasicInfo.Email || x.FullName == VehicleOwnerBasicInfo.FullName).FirstOrDefault();
                    //{
                    //    return false;
                    //}

                    var user = new ApplicationUser();
                    user.FullName = VehicleOwnerBasicInfo.Email;
                    user.UserName  = VehicleOwnerBasicInfo.Email;                   
                    user.MobileNo = VehicleOwnerBasicInfo.PhoneNumber;
                    user.PersonalEmail = VehicleOwnerBasicInfo.Email;
                    user.Email = VehicleOwnerBasicInfo.Email;
                    user.EmailConfirmed = true;
                    
                    await _context.Users.AddAsync(user);
                    await _userManager.CreateAsync(user,model.Password);
                    _context.SaveChanges();
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                return false;

            }

        }

        public Task<bool> DeleteOwner(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
