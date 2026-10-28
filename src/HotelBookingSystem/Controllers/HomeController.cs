using HotelBookingSystem.Services;
using HotelBookingSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers;

public class HomeController : Controller
{
    private readonly AvailabilityService _avail;
    public HomeController(AvailabilityService avail) => _avail = avail;

    public async Task<IActionResult> Index(SearchVm vm)
    {
        if (vm.CheckIn.HasValue || vm.CheckOut.HasValue)
        {
            vm.Searched = true;
            var error = AvailabilityService.ValidateStay(vm.CheckIn, vm.CheckOut, vm.Guests);
            if (error != null) ModelState.AddModelError("", error);
            else vm.Results = await _avail.AvailableRooms(vm.CheckIn!.Value, vm.CheckOut!.Value, vm.Guests)
                                          .OrderBy(r => r.PricePerNight).ToListAsync();
        }
        return View(vm);
    }

    public IActionResult Error() => View();
}
