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

        #region Owner
        public async Task<IActionResult> OwnerIndex()
        {
            return View(await _common.GetAllOwner());
        }
        public async Task<IActionResult> CreateOwner(int id = 0)
        {
            return View(await _common.GetVehicleOwenerById(id));
        }
        public async Task<IActionResult> OwnerDetails(int id = 0)
        {
            return View(await _common.GetVehicleOwenerById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateOwner(VehicleOwnerViewModel model)
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
    }
}

