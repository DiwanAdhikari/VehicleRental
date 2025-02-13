using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleRentalSystem.Data;
using VehicleRentalSystem.Interface;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Controllers
{
    [Authorize]
    public class CommonController : Controller
    {
        private readonly ICommon _common = null;
        private readonly ApplicationDbContext _context = null;
        //private readonly IUtility _utility;
        public CommonController(ICommon common, ApplicationDbContext context)
        {
            _common = common;
            _context = context;

        }
        #region Category
        public async Task<IActionResult> CategoryIndex()
        {
            return View(await _common.GetAllCategory());
        }
        public async Task<IActionResult> CreateCategory(int id = 0)
        {
            return View(await _common.GetCategoryById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdate(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("CategoryIndex");
                }
            }
            return View(model);
        }
        #endregion
        #region Brand
        public async Task<IActionResult> BrandIndex()
        {
            return View(await _common.GetAllBrand());
        }
        public async Task<IActionResult> CreateBrand(int id = 0)
        {
            return View(await _common.GetBrandById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(BrandViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateBrand(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("BrandIndex");
                }
            }
            return View(model);
        }
        #endregion

        #region Owner
        public async Task<IActionResult> OwnerIndex()
        {
            return View(await _common.GetAllOwner());
        }
        [AllowAnonymous]
        public async Task<IActionResult> CreateOwner(int id = 0)
        {
            return View(await _common.GetVehicleOwenerById(id));
        }
        public async Task<IActionResult> OwnerDetails(int id = 0)
        {
            return View(await _common.GetVehicleOwenerById(id));
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromForm] VehicleOwnerViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateOwner(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("OwnerIndex");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteOwner(int id)
        {
            TempData["Msg"] = await _common.DeleteOwner(id) ? "Deleted Successfully!!" : "Try Again Later";
            return RedirectToAction("OwnerIndex");
        }
        #endregion 
        #region VehicleRenter
        public async Task<IActionResult> VehicleRenterIndex()
        {
            return View(await _common.GetAllRenter());
        }
        public async Task<IActionResult> CreateVehicleRenter(int id = 0)
        {
            return View(await _common.GetRenterById(id));
        }
        public async Task<IActionResult> VehicleRenterDetails(int id = 0)
        {
            return View(await _common.GetRenterById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicleRenter(RenterViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateRenter(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("OwnerIndex");
                }
            }
            return View(model);
        }
        #endregion   
        #region Vehicle
        public async Task<IActionResult> VehicleIndex()
        {
            return View(await _common.GetAllVehicles());
        }
        public async Task<IActionResult> CreateVehicle(int id = 0)
        {
            return View(await _common.GetVehiclesById(id));
        }
        public async Task<IActionResult> VehicleDetails(int id = 0)
        {
            return View(await _common.GetVehiclesById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle(VehicleViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateVehicle(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("VehicleIndex");
                }
            }
            return View(model);
        }
        #endregion
        #region Booking
        public async Task<IActionResult> BookingIndex()
        {
            return View(await _common.GetAllBooking());
        }
        public async Task<IActionResult> Booking(int id = 0)
        {
            return View(await _common.GetBookingById(id));
        }
        public async Task<IActionResult> BookingDetails(int id = 0)
        {
            return View(await _common.GetVehiclesById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Booking(BookingViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateBooking(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("BookingIndex");
                }
            }
            return View(model);
        }
        #endregion
    }
}

