using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentalSystem.Models
{

    public class GenderViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Field is required.")]
        [RegularExpression(@"\S.*", ErrorMessage = "Field cannot contain only spaces.")]
        public string Name { get; set; }
    }


    public class RenterViewModel
    {

        public int Id { get; set; }
        public int GenderId { get; set; }
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string CitizenshipNumber { get; set; }
        public string LicenseNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string GenderName { get; set; }
        public string Address { get; set; }
        public string ProfilePictureUrl { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public bool IsActive { get; set; }


    }
    public class VehicleViewModel
    {

        public int Id { get; set; }
        public int VehicleOwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public decimal RentalPrice { get; set; }
        public string Availability { get; set; }
        public string Terms { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }

        public bool IsActive { get; set; }
    }

    public class BookingViewModel
    {

        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int RenterId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public string VehicleName { get; set; }
        public string RenterName { get; set; }

    }
    public class PaymentViewModel
    {

        public int Id { get; set; }
        public int BookingId { get; set; }

        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }

    }
    public class ReviewViewModel
    {

        public int Id { get; set; }
        public int RenterId { get; set; }
        public int VehicleId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public string RenterName { get; set; }
        public string VehicleName { get; set; }

    }
    public class BrandViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string CategoryName { get; set; }

    }


    public class VehicleOwnerViewModel
    {
        public int VehicleOwnerId { get; set; }
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public int GenderId { get; set; }

        public string GenderName { get; set; } // Not required since it's usually a display field

        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Brand is required.")]
        public int BrandId { get; set; }

        public string BrandName { get; set; } // Not required since it's usually a display field

        [Required(ErrorMessage = "Billbook Number is required.")]
        [StringLength(50, ErrorMessage = "Billbook Number cannot exceed 50 characters.")]
        public string BillbookNumber { get; set; }


        public string BillbookImage { get; set; }

        [Required(ErrorMessage = "Production Year is required.")]
        public DateOnly ProductionYear { get; set; }

        [Required(ErrorMessage = "Registration Number is required.")]
        [StringLength(50, ErrorMessage = "Registration Number cannot exceed 50 characters.")]
        public string RegistrationNumber { get; set; }


        public string VehiclePhotos { get; set; }


        public IFormFile VehiclePhotosURL { get; set; }


        public IFormFile BillbookImageURL { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }


}

