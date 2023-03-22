using VeriTakipMvc.Models;

namespace VeriTakipMvc.ViewNodels
{
    public class UserViewModel
    {
        public IEnumerable<AppUser> Users { get; set; }
        public AppUser User { get; set; }
    }
}
