using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Interface
{
    public interface ICommon
    {
        Task<List<CategoryViewModel>> GetAllCategory();
        Task<CategoryViewModel> GetCategoryById(int? id);
        Task<ResponseViewModel> InsertUpdate(CategoryViewModel model);

        Task<List<VehicleOwnerViewModel>> GetAllOwner();
        Task<VehicleOwnerViewModel> GetVehicleOwenerById(int? id);
        Task<ResponseViewModel> InsertUpdateOwner(VehicleOwnerViewModel model);


        Task<List<RenterViewModel>> GetAllRenter();
        Task<RenterViewModel> GetRenterById(int? id);
        Task<ResponseViewModel> InsertUpdateRenter(RenterViewModel model);

        Task<List<VehicleViewModel>> GetAllVehicles();
        Task<VehicleViewModel> GetVehiclesById(int? id);
        Task<ResponseViewModel> InsertUpdateVehicle(VehicleViewModel model);

        Task<List<BookingViewModel>> GetAllBooking();
        Task<BookingViewModel> GetBookingById(int? id);
        Task<ResponseViewModel> InsertUpdateBooking(BookingViewModel model);

        Task<List<PaymentViewModel>> GetAllPayment();
        Task<PaymentViewModel> GetPaymentById(int? id);
        Task<ResponseViewModel> InsertUpdatePayment(PaymentViewModel model);


        Task<List<ReviewViewModel>> GetAllReview();
        Task<ReviewViewModel> GetReviewById(int? id);
        Task<ResponseViewModel> InsertUpdateReview(ReviewViewModel model);
    }
}