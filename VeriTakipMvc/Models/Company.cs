using System.ComponentModel.DataAnnotations;

namespace VeriTakipMvc.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string? Companyname { get; set; }
        public string? CompanyEmail { get; set; }

    }
}
