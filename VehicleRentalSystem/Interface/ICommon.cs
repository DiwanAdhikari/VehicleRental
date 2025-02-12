using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Interface
{
    public interface ICommon
    {
        Task<List<CategoryViewModel>> GetAllCategory();
        Task<CategoryViewModel> GetCategoryById(int? id);
        Task<bool> InsertUpdate(CategoryViewModel model);

        Task<List<VehicleOwnerViewModel>> GetAllOwner();
        Task<VehicleOwnerViewModel> GetVehicleOwenerById(int? id);
        Task<bool> InsertUpdateOwner(VehicleOwnerViewModel model);
        Task<bool> DeleteOwner(int id);

        Task<List<RenterViewModel>> GetAllRenter();
        Task<RenterViewModel> GetRenterById(int? id);
        Task<bool> InsertUpdateRenter(RenterViewModel model);

        Task<List<VehicleViewModel>> GetAllVehicles();
        Task<VehicleViewModel> GetVehiclesById(int? id);
        Task<bool> InsertUpdateVehicle(VehicleViewModel model);

        Task<List<BookingViewModel>> GetAllBooking();
        Task<BookingViewModel> GetBookingById(int? id);
        Task<bool> InsertUpdateBooking(BookingViewModel model);

        Task<List<PaymentViewModel>> GetAllPayment();
        Task<PaymentViewModel> GetPaymentById(int? id);
        Task<bool> InsertUpdatePayment(PaymentViewModel model);


        Task<List<ReviewViewModel>> GetAllReview();
        Task<ReviewViewModel> GetReviewById(int? id);
        Task<bool> InsertUpdateReview(ReviewViewModel model);
    }
}