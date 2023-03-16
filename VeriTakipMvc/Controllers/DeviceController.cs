using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using VeriTakipMvc.Data.Interfaces;
using VeriTakipMvc.Models;
using VeriTakipMvc.ViewNodels;

namespace VeriTakipMvc.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly UserManager<AppUser> _userManager;
		private readonly ICompanyRepository _companyRepository;

		public DeviceController(IDeviceRepository deviceRepository, UserManager<AppUser> userManager, ICompanyRepository companyRepository)
		{
			_deviceRepository = deviceRepository;
			_userManager = userManager;
			_companyRepository = companyRepository;
		}

		public async Task<IActionResult> Index()
        {
            string cookieValue = Request.Cookies["jwt"];
            ViewBag.cookieValue = cookieValue;
            var company = _userManager.GetUserAsync(HttpContext.User).Result.CompanyId;
            var deviceViewModels = await _deviceRepository.GetDeviceViewModels(company);
            return View(deviceViewModels);
        }

		public IActionResult AddDevice()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddDevice(Device device)
		{
			//var company = _userManager.GetUserAsync(User).Result.CompanyId;
			//device.CompanyId = company.CompanyId;
			//_deviceRepository.AddDevice(device);
			return RedirectToAction("Index");
		}
        public async Task<IActionResult> DeviceDetail(int id)
        {
            string cookieValue = Request.Cookies["jwt"];
            ViewBag.cookieValue = cookieValue;
            var company = _userManager.GetUserAsync(HttpContext.User).Result.CompanyId;
            var deviceViewModels = await _deviceRepository.GetDeviceViewModel(company);
            return View(deviceViewModels);
        }

        public IActionResult EditDevice(int id)
		{
			var device = _deviceRepository.GetDevice(id).Result;
			return View(device);
		}


		[HttpPost]
		public IActionResult EditDevice(Device device)
		{
            var result = _deviceRepository.UpdateDevice(device);

            return View(device);
			
		}

		[HttpGet]
		public IActionResult GetDeviceList()
		{
			return RedirectToAction("Index");
		}


   
        public IActionResult Delete(int deviceId)
		{
            var result = _deviceRepository.DeleteDevice(deviceId);
            return RedirectToAction("Index");

        }

    }
}
