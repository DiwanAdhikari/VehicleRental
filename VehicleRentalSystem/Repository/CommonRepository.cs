using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
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

        public CommonRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<CommonRepository> logger, IUtility utility)
        {
            _context = context;
            _utility = utility;
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
        public async Task<ResponseViewModel> InsertUpdate(CategoryViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
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
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        #endregion
        #region VehicleOwner
        public async Task<List<VehicleOwnerViewModel>> GetAllOwner()
        {
            return await _context.VehicleOwner
                .Select(x => new VehicleOwnerViewModel()
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Address = x.Address,
                    GenderName = x.Gender.Name,
                    ProfilePictureUrl = x.ProfilePicture,
                }).ToListAsync();
        }
        public async Task<VehicleOwnerViewModel> GetVehicleOwenerById(int? id)
        {
            return await _context.VehicleOwner.Where(x => x.Id == id)
                .Select(x => new VehicleOwnerViewModel()
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Address = x.Address,
                    GenderId = x.GenderId,
                    GenderName = x.Gender.Name,
                    ProfilePictureUrl = x.ProfilePicture,
                }).FirstOrDefaultAsync() ?? new VehicleOwnerViewModel();
        }
        public async Task<ResponseViewModel> InsertUpdateOwner(VehicleOwnerViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
            var img = await _utility.UploadImgReturnPathAndName("Gaunpalika", model.ProfilePicture);
            try
            {
                if (model.Id > 0)
                {
                    var owner = await _context.VehicleOwner.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (owner != null)
                    {
                        owner.FullName = model.FullName;
                        owner.PhoneNumber = model.PhoneNumber;
                        owner.Email = model.Email;
                        owner.Address = model.Address;
                        owner.GenderId = model.GenderId;
                        owner.ProfilePicture = img.FilePath;
                        _context.Entry(owner).State = EntityState.Modified;
                    }
                }
                else
                {
                    var owneradd = new VehicleOwner()
                    {
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Address = model.Address,
                        GenderId = model.GenderId,
                        ProfilePicture = img.FilePath,

                    };
                    await _context.VehicleOwner.AddAsync(owneradd);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        #endregion
        #region VehicleRenter
        public async Task<List<RenterViewModel>> GetAllRenter()
        {
            return await _context.Renter
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
                }).FirstOrDefaultAsync() ?? new RenterViewModel();
        }
        public async Task<ResponseViewModel> InsertUpdateRenter(RenterViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
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


                    };
                    await _context.Renter.AddAsync(renteradd);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        #endregion
        #region VehicleRegister
        public async Task<List<VehicleViewModel>> GetAllVehicles()
        {
            return await _context.Vehicle
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
                }).FirstOrDefaultAsync() ?? new VehicleViewModel();
        }
        public async Task<ResponseViewModel> InsertUpdateVehicle(VehicleViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
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
                    };
                    await _context.Vehicle.AddAsync(addvehicle);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                return response;
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
        public async Task<ResponseViewModel> InsertUpdateBooking(BookingViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
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
                        book.Status = model.Status;
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
                        Status = model.Status,
                        CreatedBy = model.CreatedBy,

                    };
                    await _context.Booking.AddAsync(booking);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                return response;
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
        public async Task<ResponseViewModel> InsertUpdatePayment(PaymentViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
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
                return response;
            }
            catch (Exception ex)
            {
                return response;
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
        public async Task<ResponseViewModel> InsertUpdateReview(ReviewViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
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
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        #endregion
    }
}
