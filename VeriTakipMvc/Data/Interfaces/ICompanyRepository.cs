using VeriTakipMvc.Models;

namespace VeriTakipMvc.Data.Interfaces
{
	public interface ICompanyRepository
	{
		Task CreateCompany(Company company);
		Task DeleteCompany(int id);
		Task<Company> GetCompany(int id);
		Task UpdateCompany(Company company);
        Task<List<Company>> GetCompanies();
    }
}
