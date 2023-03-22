using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeriTakipMvc.Data.Interfaces;
using VeriTakipMvc.Models;
using VeriTakipMvc.Services.Interfaces;
using VeriTakipMvc.ViewNodels;

namespace VeriTakipMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILoginRepository _userService;
        private readonly IJwtService _jwtService;
        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILoginRepository userService, IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _jwtService = jwtService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserLoginModel user)
        {
           
            if (ModelState.IsValid)
            {
                var result = _userService.LoginUser(user, _signInManager);
                if (result.Result.Succeeded)
                {
                    var token = _jwtService.GenerateJwtToken(user.Username, "elitelektronik.net");
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddHours(1)
                    };
                    Response.Cookies.Append("jwt", token, cookieOptions);

                    HttpContext.Session.SetString("jwt", token);
					return Redirect("/Device/Index/");
                }
                else
                {
                    //var user1 = _userManager.GetUserAsync(User).Result;
                    return Redirect("hataligiris");
                }
            }
            //if the values ​​from the form are unvalid
            return Redirect("https://www.google.com.tr");
        }
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Redirect("/User/Index/");
		}


		[AllowAnonymous]
        [HttpPost]
        public IActionResult SignUp(UserRegisterModel model)
        {
            //if the values ​​from the form are valid
            if (ModelState.IsValid)
            {
                //checking is the username exists
                AppUser user = new()
                {
                    Email = model.Email,
                    UserName = model.Username
                };

                var result = _userService.RegisterUser(model, _userManager);
                //var result = await _userManager.CreateAsync(user, pa.Password);
                //field to enter if registration is successful
                if (result.Result.Succeeded)
                {
                    return Redirect("/User/Index/");
                }
                //field to enter if registration is failed
                else
                {
                    foreach (var item in result.Result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                //if the values ​​from the form are unvalid
            }
            else
            {
                return Redirect("https://www.google.com.tr");
            }
            return Redirect("https://www.google.com.tr");
        }
    }
}
