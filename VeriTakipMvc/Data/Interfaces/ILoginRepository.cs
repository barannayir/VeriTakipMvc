using Microsoft.AspNetCore.Identity;
using VeriTakipMvc.Models;
using VeriTakipMvc.ViewNodels;

namespace VeriTakipMvc.Data.Interfaces
{
    public interface ILoginRepository
    {
        Task<SignInResult> LoginUser(UserLoginModel model, SignInManager<AppUser> signInManager);
        Task<IdentityResult> RegisterUser(UserRegisterModel model, UserManager<AppUser> userManager);
    }
}
