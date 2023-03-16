using Microsoft.AspNetCore.Identity;

namespace VeriTakipMvc.Models
{
    public class AppUser : IdentityUser<int>
    {
    public int CompanyId { get; set; }

    public List<UserOfLevel>? UserOfLevels { get; set; } = new List<UserOfLevel>();

	}


}
