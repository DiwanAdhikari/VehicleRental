using Microsoft.AspNetCore.Identity;

namespace VehicleRentalSystem.Data
{
    public class ApplicationUser: IdentityUser

    {
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string PersonalEmail { get; set; }
    }
}
