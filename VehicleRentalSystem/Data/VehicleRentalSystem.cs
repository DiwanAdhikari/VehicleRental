using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRentalSystem.Data
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class VehicleOwner
    {
        [Key]
        public int Id { get; set; }
        public int GenderId { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
    }
    public class Renter
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string CitizenshipNumber { get; set; }
        public string Email { get; set; }
        public int GenderId { get; set; }
        public string LicenseNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime DateOfBirth { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
     

    }
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public int VehicleOwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal RentalPrice { get; set; }
        public string Availability { get; set; } 
        public string Terms { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("VehicleOwnerId")]
     
        public VehicleOwner Owner { get; set; }

    }
  
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int RenterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } 
        public DateTime CreatedBy { get; set; }
        [ForeignKey("VehicleId")]
      
        public Vehicle Vehicle { get; set; }
        [ForeignKey("RenterId")]
        public Renter Renter { get; set; }
    }
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int BookingId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }
    }
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int RenterId { get; set; }
        public int VehicleId { get; set; }
        public int Rating { get; set; } 
        public string Comments { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
        [ForeignKey("RenterId")]
        public Renter Renter { get; set; }
    }
}
