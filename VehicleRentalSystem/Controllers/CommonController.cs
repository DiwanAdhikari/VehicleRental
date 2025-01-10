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
        public async Task<IActionResult> CategoryIndex()
        {
            return View(await _common.GetAllCategory());
        }
        public async Task<IActionResult> Create(int id = 0)
        {
            return View(await _common.GetCategoryById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                var response = await _common.InsertUpdate(model);
                if (response.Status)
                {
                    TempData["success"] = "Successful";
                    return RedirectToAction("Index");
                }
            }
            TempData["error"] = "Try again";
            return View(model);

        }
    }
}
