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

    public class VehicleOwnerViewModel
    {

        public int Id { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public string FullName { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string GenderName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public IFormFile ProfilePicture { get; set; }


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


    }

    public class BookingViewModel
    {

        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int RenterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedBy { get; set; }

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
}
