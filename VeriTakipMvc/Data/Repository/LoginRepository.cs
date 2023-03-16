using Microsoft.AspNetCore.Identity;
using VeriTakipMvc.Data.Interfaces;
using VeriTakipMvc.Models;
using VeriTakipMvc.ViewNodels;

namespace VeriTakipMvc.Data.Repository
{
	public class LoginRepository : ILoginRepository
	{
		public async Task<SignInResult> LoginUser(UserLoginModel model, SignInManager<AppUser> _signInManager)
		{
			return await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, true);
		}

		public async Task<IdentityResult> RegisterUser(UserRegisterModel model, UserManager<AppUser> _userManager)
		{
			var user = new AppUser { UserName = model.Username };
			return await _userManager.CreateAsync(user, model.Password);

		}
	}
}
