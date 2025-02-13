using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }

    public class VehicleOwnerBasicInfo
    {
        [Key]
        public int Id { get; set; }     
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Email { get; set; }
        public int GenderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }        
        public bool IsActive { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
    }
   
    public class VehicleDetails
    {
        public int Id { get; set; }
        public int VehicleOwnerBasicInfoId { get; set; }
        public int BrandId { get; set; }
        public string BillbookNumber { get; set; }
        public string BillbookImage { get; set; }
        public DateOnly ProductionYear { get; set; }
        public string RegistrationNumber { get; set; }
        public string VehiclePhotos { get; set; }
        [ForeignKey("VehicleOwnerBasicInfoId")]
        public VehicleOwnerBasicInfo VehicleOwnerBasicInfo { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
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
        public string LicensePhoto { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
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
        public bool IsActive { get; set; }
      

    }
  
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int RenterId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool Status { get; set; } 
        public string CreatedBy { get; set; }

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
