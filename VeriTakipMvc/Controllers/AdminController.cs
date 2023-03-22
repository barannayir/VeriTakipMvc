using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeriTakipMvc.Data.Interfaces;
using VeriTakipMvc.Models;
using VeriTakipMvc.ViewNodels;

namespace VeriTakipMvc.Controllers
{
	

	public class AdminController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyRepository _companyRepository;
        private readonly IDeviceRepository _deviceRepository;

        public AdminController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, ICompanyRepository companyRepository, IDeviceRepository deviceRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _companyRepository = companyRepository;
            _deviceRepository = deviceRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

		[Authorize(Roles = "admin")]
		public async Task<IActionResult> CompanyAsync()
        {
          
            var model = new CompanyViewModel()
            {
                Companies = await _companyRepository.GetCompanies(),
                Company = new Company()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult AddCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.CreateCompany(company);
                return RedirectToAction("Company");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditCompany(int id)
        {
            var company = _companyRepository.GetCompany(id).Result;
            return View(company);
        }

        [HttpPost]
        public IActionResult UpdateCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.UpdateCompany(company);
                return RedirectToAction("Company");
            }
            return View(company);
        }

        [HttpPost]
        public IActionResult DeleteCompany(int id)
        {
            _companyRepository.DeleteCompany(id);
            return RedirectToAction("Company");
        }

        public async Task<IActionResult> Device()
        {
            var model = new AdminDeviceVM()
            {
                Devices = await _deviceRepository.GetDevices(),
                Device = new Device()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddDevice(Device device)
        {
            if (ModelState.IsValid)
            {
                _deviceRepository.CreateDevice(device);
                return RedirectToAction("Device");
            }
            return View();
        }

        public IActionResult EditDevice(int id)
        {
            var device = _deviceRepository.GetDevice(id).Result;
            return View(device);
        }

        [HttpPost]
        public IActionResult UpdateDevice(Device device)
        {
            if (ModelState.IsValid)
            {
                _deviceRepository.UpdateDevice(device);
                return RedirectToAction("Device");
            }
            return View(device);
        }

        [HttpPost]
        public IActionResult DeleteDevice(int id)
        {
            _deviceRepository.DeleteDevice(id);
            return RedirectToAction("Device");
        }

        

        
        public async Task<IActionResult> User()
        {
            var model = new UserViewModel()
            {
                Users = _userManager.Users.ToList(),
                User = new AppUser()
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AppUser user)
        {
            if (ModelState.IsValid)
            {
                await _userManager.CreateAsync(user);
                return RedirectToAction("User");
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(AppUser user)
        {
            if (ModelState.IsValid)
            {
                await _userManager.UpdateAsync(user);
                return RedirectToAction("User");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("User");
        }

        

    }
}
