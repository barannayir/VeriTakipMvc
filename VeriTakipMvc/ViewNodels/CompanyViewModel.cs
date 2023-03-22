using VeriTakipMvc.Models;

namespace VeriTakipMvc.ViewNodels
{
    public class CompanyViewModel
    {
        public IEnumerable<Company> Companies { get; set; }
        public Company Company { get; set; }
    }
}
